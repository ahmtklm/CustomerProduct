using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerProduct.Data.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [ForeignKey("Customer")]
        public string IdentificationNumber { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal Price { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
