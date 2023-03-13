using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Domain
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool HasAlcohol { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public ICollection<Package>? Packages { get; set; }
    }
}
