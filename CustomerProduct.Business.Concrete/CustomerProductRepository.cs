using CustomerProduct.Business.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CustomerProduct.Business.Concrete
{
    public class CustomerProductRepository : GenericRepository<Data.Entities.CustomerProduct>, ICustomerProductRepository
    {
        public CustomerProductRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
