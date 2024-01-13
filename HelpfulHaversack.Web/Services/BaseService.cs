using HelpfulHaversack.Web.Models.Dto;
using HelpfulHaversack.Web.Services.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HelpfulHaversack.Web.Services
{
    public class BaseService : IBaseService
    {
        //Dependency Injection
        private readonly IHttpClientFactory _httpClientFactory;

        //Constructors
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        //End Dependency Injection

        public async Task<ResponseDto?> SendAsync(RequestDto request)
        {
            try
            {
                //Create HTTP Client
                HttpClient client = _httpClientFactory.CreateClient("TreasuryAPI");

                //Prepare HTTP Request
                HttpRequestMessage message = new();

                //Add a key-value pair to the header of the request
                message.Headers.Add("Accept", "application/json");

				//token

				//Set the URI to make a request to
				message.RequestUri = new Uri(request.ApiUrl);

				//If the request DTO has valid data, serialize it into JSON format and add it 
				//to the request conent as a string
				if (request.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(request.Data), //object
                        Encoding.UTF8, //encoding
                        "application/json"); //media type
                }

                HttpResponseMessage? apiResponse = null;

                //Set the method for the request according to the DTO
                message.Method = request.ApiType switch
                {
                    Util.StaticDetails.ApiType.POST => HttpMethod.Post,

                    Util.StaticDetails.ApiType.PUT => HttpMethod.Put,

                    Util.StaticDetails.ApiType.DELETE => HttpMethod.Delete,

                    _ => HttpMethod.Get,
                };

                //Send the request via the HTTP Client
                apiResponse = await client.SendAsync(message);

                //If the status code of the response inidcates an error, set the success flag to 
                //false and the message accordingly, otherwise deserialize the response content
                //into a DTO and return it.
                return apiResponse.StatusCode switch
                {
                    HttpStatusCode.NotFound =>
                        new() { IsSuccess = false, Message = "Not Found" },

                    HttpStatusCode.Forbidden =>
                        new() { IsSuccess = false, Message = "Access Denied" },

                    HttpStatusCode.Unauthorized =>
                        new() { IsSuccess = false, Message = "Unauthorized" },

                    HttpStatusCode.InternalServerError =>
                        new() { IsSuccess = false, Message = "Internal Server Error" },

                    _ =>
                        JsonConvert.DeserializeObject<ResponseDto>(
                                            await apiResponse.Content.ReadAsStringAsync()),
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

    }
}
