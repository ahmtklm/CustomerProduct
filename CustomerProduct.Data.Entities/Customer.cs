using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerProduct.Data.Entities
{
    public class Customer
    {
        public Customer()
        {
            Products = new List<Product>();
        }

        [Key]
        public string IdentificationNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
