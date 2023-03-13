using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Domain
{
    public class Canteen
    {
        [Key]
        public int CanteenId { get; set; }
        public string City { get; set; }
        public Location Location { get; set; }
        public bool IsWarm { get; set; }
        [JsonIgnore]
        public List<Package>? Packages { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
