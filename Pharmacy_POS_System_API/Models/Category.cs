using System.Text.Json.Serialization;

namespace Pharmacy_POS_System_API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        [JsonIgnore]
        public Brand Brand { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
