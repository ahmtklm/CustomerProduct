using System.Collections.Generic;

namespace CustomerProduct.Common.EntityResponseStructure
{
    public class ServiceEntityResponse<TRequestType>
    {
        public int  ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        private List<TRequestType> _entityDataList = new List<TRequestType>();

        public List<TRequestType> EntityDataList
        {
            get { return _entityDataList; }

            set { _entityDataList = value; }
        }

        public TRequestType EntityData { get; set; }
    }
}
