namespace CustomerProduct.Hosting.API.Models.ResponseModels
{
    public class CustomerProductResponse
    {
        public int ProductId { get; set; }

        public string  ProductName  { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
