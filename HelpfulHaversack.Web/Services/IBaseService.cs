using HelpfulHaversack.Web.Models.Dto;

namespace HelpfulHaversack.Web.Services
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto request);
    }
}
