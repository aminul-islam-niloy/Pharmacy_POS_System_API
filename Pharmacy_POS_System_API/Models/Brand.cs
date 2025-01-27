using System.Text.Json.Serialization;

namespace Pharmacy_POS_System_API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Category> Categories { get; set; }
    }
}
