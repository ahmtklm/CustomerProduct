using CustomerProduct.Business.Contracts;
using CustomerProduct.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerProduct.Business.Concrete
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {
        }

    }
}
