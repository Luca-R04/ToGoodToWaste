using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
        public string City { get; set; }
        public Canteen Canteen { get; set; }
        private DateTime _pickupTime { get; set; }
        public DateTime PickupTime
        {
            get => _pickupTime;
            set
            {
                if (DateTime.Now.Date.AddDays(2) < value.Date)
                {
                    throw new InvalidOperationException("Datum mag maximaal over 2 dagen zijn");
                }
                _pickupTime = value;
            }
        }
        public DateTime EndTime { get; set; }
        public bool ForAdult { get; set; }
        public double Price { get; set; }
        public Categories Category { get; set; }
        public Student? ForStudent { get; set; }
        public string? Image { get; set; }
    }

    public enum Categories
    {
        Brood = 1,
        Warm = 2,
        Drank = 3,
    }
}