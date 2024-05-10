using HelpfulHaversack.Web.Models.Dto;
using HelpfulHaversack.Web.Services.IService;
using HelpfulHaversack.Web.Util;
using Microsoft.AspNetCore.JsonPatch;

namespace HelpfulHaversack.Web.Services
{
    public class TreasuryService : ITreasuryService
    {
        //Dependency Injection
        private readonly IBaseService _baseService;
        private readonly string _urlBase;

        //Constructors
        public TreasuryService(IBaseService baseService)
        {
            _baseService = baseService;
            _urlBase = StaticDetails.TreasuryApiBase + "/api/TreasuryAPI/";
        }
        //End Dependency Injection

        //Methods
        public async Task<ResponseDto?> GetAllTreasuriesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + "treasuries"
            });
        }

        public async Task<ResponseDto?> GetTreasuryAsync(Guid treasuryId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"treasuries/{treasuryId}" 
            });
        }

        public async Task<ResponseDto?> SearchTreasuriesByNameAsync(string treasuryName)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"treasuries/search={treasuryName}"
            });
        }

        public async Task<ResponseDto?> GetAllItemsFromTreasuryAsync(Guid treasuryId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"treasuries/{treasuryId}/inventory"
            }) ;
        }

        public async Task<ResponseDto?> GetItemFromTreasuryAsync(Guid treasuryId, Guid itemId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"treasuries/{treasuryId}/inventory/{itemId}"
            });
        }

        public async Task<ResponseDto?> SearchItemByNameFromTreasuryAsync(Guid treasuryId, string itemName)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"treasuries/{treasuryId}/inventory/search={itemName}"
            });
        }

        public async Task<ResponseDto?> GetAllItemTemplatesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"templates"
            });
        }

        public async Task<ResponseDto?> GetItemTemplateAsync(string templateName)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"templates/{templateName}"
            });
        }

        public async Task<ResponseDto?> SearchItemTemplateAsync(string templateName)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                ApiUrl = _urlBase + $"templates/search={templateName}"
            });
        }

        public async Task<ResponseDto?> CreateTreasuryAsync(string treasuryName)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                ApiUrl = _urlBase + $"create/treasury:{treasuryName}"
            });
        }

        public async Task<ResponseDto?> CreateItemTemplateAsync(ItemTemplateDto templateDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = templateDto,
                ApiUrl = _urlBase + $"create/template:{templateDto.Name}"
            });
        }

        public async Task<ResponseDto?> UpdateItemTemplateAsync(string templateName, ItemTemplateDto itemTemplateDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = itemTemplateDto,
                ApiUrl = _urlBase + $"templates/{templateName}"
            });
        }

        public async Task<ResponseDto?> UpdateTreasuryAsync(Guid treasuryId, TreasuryDto treasuryDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = treasuryDto,
                ApiUrl = _urlBase + $"treasuries/{treasuryId}"
            });
        }

        public async Task<ResponseDto?> UpdateItemAsync(Guid treasuryId, Guid itemId, ItemDto itemDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = itemDto,
                ApiUrl = _urlBase + $"treasuries/{treasuryId}/inventory/{itemId}"
            });
        }

        public async Task<ResponseDto?> UpdateItemTemplatePartialAsync(string templateName, JsonPatchDocument<ItemTemplateDto> patchDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PATCH,
                Data = patchDto,
                ApiUrl = _urlBase + $"templates/{templateName}"
            });;
        }

        public async Task<ResponseDto?> UpdateTreasuryPartialAsync(Guid treasuryId, JsonPatchDocument<TreasuryDto> patchDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PATCH,
                Data = patchDto,
                ApiUrl = _urlBase + $"treasuries/{treasuryId}"
            });
        }

        public async Task<ResponseDto?> UpdateItemPartialAsync(Guid treasuryId, Guid itemId, JsonPatchDocument<ItemDto> patchDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PATCH,
                Data = patchDto,
                ApiUrl = _urlBase + $"/api/"
            });
        }

        public async Task<ResponseDto?> MoveItemAsync(Guid srcTreasuryId, Guid itemId, Guid destTreasuryId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PATCH,
                ApiUrl = _urlBase + $"treasuries/{srcTreasuryId}/inventory/{itemId}:sendTo={destTreasuryId}"
            });
        }

        public async Task<ResponseDto?> DeleteTreasuryAsync(Guid treasuryId)
        {
            return await _baseService.SendAsync(new RequestDto() 
            {
                ApiType = StaticDetails.ApiType.DELETE,
                ApiUrl = _baseService + $"treasuries/{treasuryId}"
            });
        }

        public async Task<ResponseDto?> DeleteItemAsync(Guid treasuryId, Guid itemId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                ApiUrl = _baseService + $"treasuries/{treasuryId}/inventory/{itemId}"
            });
        }

        public async Task<ResponseDto?> DeleteItemTemplateAsync(string templateName)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                ApiUrl = _baseService + $"templates/{templateName}"
            });
        }
    }
}
