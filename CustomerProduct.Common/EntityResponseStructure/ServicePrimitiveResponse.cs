using System;

namespace CustomerProduct.Common.EntityResponseStructure
{
    public class ServicePrimitiveResponse
    {
        public int ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public string EntityPrimaryKey { get; set; }

        public string PrimitiveResponseValue { get; set; }

        public Exception InnerException { get; set; }
    }
}
