using CustomerProduct.Data.Entities;
using System;

namespace CustomerProduct.Common
{
    [Serializable]
    public class CustomerProductModel
    {
        public CustomerProductModel()
        {
            Product = new Product();
            Customer = new Customer();
        }

        public Product Product { get; set; }

        public Customer Customer { get; set; }
    }
}
