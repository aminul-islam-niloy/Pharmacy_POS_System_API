namespace Pharmacy_POS_System_API.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public string Generic { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public IFormFile Image { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}
