using HelpfulHaversack.Web.Models.Dto;
using HelpfulHaversack.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HelpfulHaversack.Web.Controllers
{
	public class TreasuryController : Controller
	{
		//Dependency Injection
		private readonly ITreasuryService _treasuryService;

		//Constructors
		public TreasuryController(ITreasuryService treasuryService)
		{
			_treasuryService = treasuryService;
		}
		//End Dependency Injection

		public async Task<IActionResult> TreasuryIndex()
		{
			List<TreasuryDto>? list = new();

			ResponseDto? response = await _treasuryService.GetAllTreasuriesAsync();

			if(response != null && response.IsSuccess)
			{
				var responseString = Convert.ToString(response.Result);
				if(responseString != null)
					list = JsonConvert.DeserializeObject<List<TreasuryDto>>(responseString);
			}

			return View(list);
		}
	}
}
