using HelpfulHaversack.Services.ContainerAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace HelpfulHaversack.Services.ContainerAPI.Controllers
{
	[ApiController]
	[Route("Encryption")]
	public class EncryptionController : Controller
	{
		[HttpGet]
		public EncryptionResponse Get()
		{
			var rsaHelper = RsaHelper.Instance;
			return new EncryptionResponse
			{
				Encrypted = "",
				Decrypted = ""
			};
		}
	}
}
