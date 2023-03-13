using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Building { get; set; }
        public string Image { get; set; }
    }
}
