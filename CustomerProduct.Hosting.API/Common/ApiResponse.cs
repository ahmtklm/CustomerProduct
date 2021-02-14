using Newtonsoft.Json;
using System.Collections.Generic;

namespace CustomerProduct.Hosting.API.Common
{
    public abstract class IApiResponse
    {

        public IApiResponse() { }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ValidationErrors { get; set; }


        public int Code { get; set; }

        public string ErrorMessage { get; set; }

    }

    public class ApiResponse : IApiResponse { }

    public class ApiResponse<T> : IApiResponse
    {
        public ApiResponse()
        {}

        public ApiResponse(int responseCode, T responseData)
        {
            Code = responseCode;
            Data = responseData;
        }

        public T Data { get; set; }


    }


}
