using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerProduct.Data.Entities
{
    public class CustomerProduct
    {
        [Key]
        public int CustomerProductId { get; set; }

        [ForeignKey("Customer")]
        public string IdentificationNumber { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Customer Customer{ get; set; }


    }
}
