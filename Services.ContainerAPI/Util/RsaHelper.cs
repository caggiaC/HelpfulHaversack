using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models.Dto;
using Newtonsoft.Json;
using Services.ContainerAPI.Models.Dto;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace HelpfulHaversack.Services.ContainerAPI.Util
{
    /// <summary>
    /// A singleton class responsible for encrypting and decrypting messages for transit.
    /// </summary>
    public class RsaHelper
	{
		private static readonly Lazy<RsaHelper> _instance = new(() => new RsaHelper());

		/// <summary>
		/// The RsaHelper singleton instance
		/// </summary>
		public static RsaHelper Instance { get { return _instance.Value; } }

		private static readonly RSACryptoServiceProvider _cryptoServiceProvider = new();
		private readonly RSAParameters _privateKey;
		private readonly RSAParameters _publicKey;

		private RsaHelper()
		{
			_privateKey = _cryptoServiceProvider.ExportParameters(true);
			_publicKey = _cryptoServiceProvider.ExportParameters(false);
		}

		/// <summary>
		/// Returns the current public encryption key
		/// </summary>
		/// <returns>A string representation of the public encryption key</returns>
		public string GetPublicKey()
		{
			var stringWriter = new StringWriter();
			var xmlSeralizer = new XmlSerializer(typeof(RSAParameters));

			xmlSeralizer.Serialize(stringWriter, _publicKey);
			return stringWriter.ToString();
		}

		/// <summary>
		/// Takes a string and encrypts it byte-by-byte using the provided key
		/// </summary>
		/// <param name="plainText">The text to encrypt</param>
		/// <param name="targetKey">The RSA key to encrypt the text with</param>
		/// <returns>A string representation of the encrypted text</returns>
		public string Encrypt(string plainText, RSAParameters targetKey)
		{
			_cryptoServiceProvider.ImportParameters(targetKey);

			var cypher = _cryptoServiceProvider.Encrypt(Encoding.Unicode.GetBytes(plainText), false);

			return Convert.ToBase64String(cypher);
		}

		/// <summary>
		/// Takes an encrypted string and attempts to decrypt it using the private key
		/// </summary>
		/// <param name="cypherText">The encrypted text to be decrypted</param>
		/// <returns>A string representation of the encrypted text</returns>
		public string Decrypt(string cypherText)
		{
			_cryptoServiceProvider.ImportParameters(_privateKey);

			var plainText = _cryptoServiceProvider.Decrypt(Convert.FromBase64String(cypherText), false);

			return Encoding.Unicode.GetString(plainText);
		}

        /// <summary>
        /// Converts a ResponseDto into an EncryptionResponse that can be safely sent across
        /// a network.
        /// </summary>
        /// <param name="obj">The object to be encrypted.</param>
        /// <param name="targetKey">The public RSA key for the destination service.</param>
        /// <returns>
        /// An EncrpytionResponse containing two representations of the ResponseDto;
        /// one encrypted with the target's public key, and one encrypted with this instance's
        /// private key for verification purposes.
        /// </returns>
        public EncryptionDto EncryptResponse(object? obj, RSAParameters targetKey)
        {
            string plainText = (obj != null) ? JsonConvert.SerializeObject(obj) : "null";

            return new EncryptionDto
            {
                Data = Encrypt(plainText, targetKey),
                Verification = Encrypt(plainText, _privateKey)
            };
        }
    }
}
