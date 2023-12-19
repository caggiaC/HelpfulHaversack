
using HelpfulHaversack.Web.Models.Dto;

namespace HelpfulHaversack.Web.Services.IService
{
    public interface ITreasuryService
    {
        Task<ResponseDto>? GetAllTreasuriesAsync();
        Task<ResponseDto>? GetTreasuryAsync(Guid treasuryId);
        Task<ResponseDto>? SearchTreasuriesByNameAsync(string treasuryName);
        Task<ResponseDto>? GetAllItemsFromTreasuryAsync(Guid treasuryId);
        Task<ResponseDto>? GetAllItemsFromTreausyAsync(Guid treasuryId);
        Task<ResponseDto>? GetItemFromTreasuryAsync(Guid treasuryId, Guid itemId);
        Task<ResponseDto>? SearchItemByNameFromTreasuryAsync(Guid treasuryId, string itemName);
        Task<ResponseDto>? GetAllItemTemplatesAsync();
        Task<ResponseDto>? GetItemTemplateAsync(string templateName);
        Task<ResponseDto>? SearchItemTemplateAsync(string templateName);
    }
}
