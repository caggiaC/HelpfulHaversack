using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace HelpfulHaversack.Services.ContainerAPI.Util
{
	public class RsaHelper
	{
		private static RSACryptoServiceProvider _cryptoServiceProvider = new();
		private readonly RSAParameters _privateKey;
		private readonly RSAParameters _publicKey;

		public RsaHelper()
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
		/// Takes a string and encrypts it byte-by-byte 
		/// </summary>
		/// <param name="plainText">The text to encrypt</param>
		/// <returns>A string representation of the encrypted text</returns>
		public string Encrypt(string plainText)
		{
			_cryptoServiceProvider.ImportParameters(_publicKey);

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
	}
}
