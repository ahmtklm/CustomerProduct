using CustomerProduct.Data.Contracts;
using CustomerProduct.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerProduct.Data.DataRepositories
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(DbContext dbContext) :base(dbContext)
        {
        }

    }
}
