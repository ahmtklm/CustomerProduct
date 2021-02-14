using CustomerProduct.Hosting.API.Validator;
using System.ComponentModel.DataAnnotations;

namespace CustomerProduct.Hosting.API.Models.RequestModels
{
    public class CustomerCreateOrDeleteRequest
    {
        public string IdentificationNumber { get; set; }

        public int ProductId { get; set; }
    }


    public class CustomerProductCreateRequest : CustomerCreateOrDeleteRequest
    {
    }

    public class CustomerProductDeleteRequest : CustomerCreateOrDeleteRequest
    {
    }

}
