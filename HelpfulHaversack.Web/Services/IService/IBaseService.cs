using HelpfulHaversack.Web.Models.Dto;

namespace HelpfulHaversack.Web.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto request);
    }
}
