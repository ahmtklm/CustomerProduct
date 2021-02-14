using System;

namespace CustomerProduct.Hosting.API.Common
{
    [Serializable]
    public class ApiException : Exception
    {
        public int ResponseCode { get; set; }

        public ApiException(int responseCode)
        {
            ResponseCode = responseCode;
        }

        public ApiException(int responseCode,string message):base(message)
        {
            ResponseCode = responseCode;
        }

    }
}
