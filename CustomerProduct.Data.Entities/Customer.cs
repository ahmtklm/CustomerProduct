using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerProduct.Data.Entities
{
    public class Customer
    {
        public Customer()
        {
            //Products = new List<Product>();
            CustomerProduct = new List<CustomerProduct>();
        }

        [Key]
        public string IdentificationNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        //public virtual ICollection<Customer> Products { get; set; }

        public virtual ICollection<CustomerProduct> CustomerProduct { get; set; }

    }
}
