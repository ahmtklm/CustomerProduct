using CustomerProduct.Data.Contracts;
using CustomerProduct.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerProduct.Data.DataRepositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {
        }

    }


}
