using CustomerProduct.Common;
using CustomerProduct.Common.EntityResponseStructure;

namespace CustomerProduct.Business.Contracts
{
    public interface ICustomerProductRepository : IGenericRepository<Data.Entities.CustomerProduct>
    {
        ServicePrimitiveResponse InsertCustomerProduct(string identificationNumber, int productId);

        ServiceEntityResponse<CustomerProductModel> GetSelectedProductsByIdentificationNumber(string identificationNumber);
    }
}
