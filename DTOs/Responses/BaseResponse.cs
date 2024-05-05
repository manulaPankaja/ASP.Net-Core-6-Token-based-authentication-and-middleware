using System.Net;
using System.Text.Json;

namespace Token_based_authentication_and_middleware.DTOs.Responses
{
    public class BaseResponse
    {
        public int status_code {  get; set; } //to return status code of the response
        public object data { get; set; } //to return data associated with the response

        public void CreateResponse(HttpStatusCode statusCode, object data)
        {
            status_code = (int)statusCode;
            this.data = data;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this); //Serialize the BaseResponse object to JSON string
        }
    }
}
