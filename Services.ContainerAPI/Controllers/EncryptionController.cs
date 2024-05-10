using HelpfulHaversack.Services.ContainerAPI.Util;
using Microsoft.AspNetCore.Mvc;
using Services.ContainerAPI.Models.Dto;
using System.Security.Cryptography;

namespace HelpfulHaversack.Services.ContainerAPI.Controllers
{
	[ApiController]
	[Route("Encryption")]
	public class EncryptionController : ControllerBase
	{
		private readonly RsaHelper _rsaHelper = RsaHelper.Instance;
		private readonly ResponseDto _response;

		//Dependency Injection

		//Constructor
		public EncryptionController()
		{
			_response = new();
		}

		//End Dependency Injection

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult GetPublicKey()
		{
			try
			{
				_response.Result = _rsaHelper.GetPublicKey();
				_response.IsSuccess = true;
			}
			catch(Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

			return Ok(_response);
		}
	}
}
