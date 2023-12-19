
using HelpfulHaversack.Web.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace HelpfulHaversack.Web.Services.IService
{
    public interface ITreasuryService
    {
        Task<ResponseDto?> GetAllTreasuriesAsync();
        Task<ResponseDto?> GetTreasuryAsync(Guid treasuryId);
        Task<ResponseDto?> SearchTreasuriesByNameAsync(string treasuryName);
        Task<ResponseDto?> GetAllItemsFromTreasuryAsync(Guid treasuryId);
        Task<ResponseDto?> GetItemFromTreasuryAsync(Guid treasuryId, Guid itemId);
        Task<ResponseDto?> SearchItemByNameFromTreasuryAsync(Guid treasuryId, string itemName);
        Task<ResponseDto?> GetAllItemTemplatesAsync();
        Task<ResponseDto?> GetItemTemplateAsync(string templateName);
        Task<ResponseDto?> SearchItemTemplateAsync(string templateName);
        Task<ResponseDto?> CreateTreasuryAsync(string treasuryName);
        Task<ResponseDto?> CreateItemTemplateAsync(ItemTemplateDto templateDto);
        Task<ResponseDto?> UpdateItemTemplateAsync(string templateName, ItemTemplateDto itemTemplateDto);
        Task<ResponseDto?> UpdateTreasuryAsync(Guid treasuryId, TreasuryDto treasuryDto);
        Task<ResponseDto?> UpdateItemAsync(Guid treasuryId, Guid itemId, ItemDto itemDto);
        Task<ResponseDto?> UpdateItemTemplatePartialAsync(string templateName, JsonPatchDocument<ItemTemplateDto> patchDto);
        Task<ResponseDto?> UpdateTreasuryPartialAsync(Guid treasuryId, JsonPatchDocument<TreasuryDto> patchDto);
        Task<ResponseDto?> UpdateItemPartialAsync(Guid treasuryId, Guid itemId, JsonPatchDocument<ItemDto> patchDto);
        Task<ResponseDto?> MoveItemAsync(Guid srcTreasuryId, Guid itemId, Guid destTreasuryId);
        Task<ResponseDto?> DeleteTreasuryAsync(Guid treasuryId);
        Task<ResponseDto?> DeleteItemTemplateAsync(string templateName);
        Task<ResponseDto?> DeleteItemAsync(Guid treasuryId, Guid itemId);
    }
}
