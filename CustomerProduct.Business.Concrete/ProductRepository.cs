using CustomerProduct.Business.Contracts;
using CustomerProduct.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerProduct.Business.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
        }

    }
}
