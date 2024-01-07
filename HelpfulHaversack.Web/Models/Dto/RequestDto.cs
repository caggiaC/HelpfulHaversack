using static HelpfulHaversack.Web.Util.StaticDetails;

namespace HelpfulHaversack.Web.Models.Dto
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; } = String.Empty;
        public object? Data { get; set; } = null;
        public string AccessToken { get; set; } = String.Empty;
    }
}