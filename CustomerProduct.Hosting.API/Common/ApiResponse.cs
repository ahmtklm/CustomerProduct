using Newtonsoft.Json;

namespace CustomerProduct.Hosting.API.Common
{
    public abstract class IApiResponse
    {
        public int Code { get; set; }

        public IApiResponse(){}

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
