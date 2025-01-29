namespace Pharmacy_POS_System_API.Models
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public string PaymentMethod { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
