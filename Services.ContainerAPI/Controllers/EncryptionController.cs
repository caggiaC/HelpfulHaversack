using HelpfulHaversack.Services.ContainerAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace HelpfulHaversack.Services.ContainerAPI.Controllers
{
	[ApiController]
	[Route("Encryption")]
	public class EncryptionController : Controller
	{
		private readonly RsaHelper _rsaHelper = RsaHelper.Instance;

		[HttpGet]
		public EncryptionResponse Get()
		{
			return new EncryptionResponse
			{
				Encrypted = "",
				Decrypted = ""
			};
		}
	}
}
