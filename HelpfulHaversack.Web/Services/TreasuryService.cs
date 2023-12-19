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

        //Constructors
        public TreasuryService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        //End Dependency Injection

        //Methods
        public Task<ResponseDto?> GetAllTreasuriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetTreasuryAsync(Guid treasuryId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> SearchTreasuriesByNameAsync(string treasuryName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetAllItemsFromTreasuryAsync(Guid treasuryId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetItemFromTreasuryAsync(Guid treasuryId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> SearchItemByNameFromTreasuryAsync(Guid treasuryId, string itemName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetAllItemTemplatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetItemTemplateAsync(string templateName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> SearchItemTemplateAsync(string templateName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> CreateTreasuryAsync(string treasuryName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> CreateItemTemplateAsync(ItemTemplateDto templateDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = templateDto,
                ApiUrl = StaticDetails.CouponApiBase + "/api/create/template:" + templateDto.Name
            });
        }

        public Task<ResponseDto?> UpdateItemTemplateAsync(string templateName, ItemTemplateDto itemTemplateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateTreasuryAsync(Guid treasuryId, TreasuryDto treasuryDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateItemAsync(Guid treasuryId, Guid itemId, ItemDto itemDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateItemTemplatePartialAsync(string templateName, JsonPatchDocument<ItemTemplateDto> patchDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateTreasuryPartialAsync(Guid treasuryId, JsonPatchDocument<TreasuryDto> patchDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> UpdateItemPartialAsync(Guid treasuryId, Guid itemId, JsonPatchDocument<ItemDto> patchDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> MoveItemAsync(Guid srcTreasuryId, Guid itemId, Guid destTreasuryId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteTreasuryAsync(Guid treasuryId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteItemAsync(Guid treasuryId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteItemTemplateAsync(string templateName)
        {
            throw new NotImplementedException();
        }
    }
}
