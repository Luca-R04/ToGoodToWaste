using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Domain
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        private DateTime _birthDate { get; set; }
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (DateTime.Now.Date < value.Date)
                {
                    throw new InvalidOperationException("Verjaardag kan niet in de toekomst zijn");
                }
                if (DateTime.Now.AddYears(-16) < value.Date)
                {
                    throw new InvalidOperationException("Leeftijd mag niet lager zijn dan 16");
                }

                _birthDate = value;
            }
        }
        public string StudentNumber { get; set; }
        public string EmailAdress { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }
        [JsonIgnore]
        public List<Package>? ReservedPackages { get; set; }
    }
}
