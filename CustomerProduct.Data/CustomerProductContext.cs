using CustomerProduct.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerProduct.Data
{
    public class CustomerProductContext : DbContext
    {

        public CustomerProductContext(DbContextOptions<CustomerProductContext> options):base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Entities.CustomerProduct> CustomerProduct { get; set; }

    }
}
