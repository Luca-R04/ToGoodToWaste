using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Core.Domain
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string EmailAdress { get; set; }
        public Location? Location { get; set; }
        [JsonIgnore]
        public Canteen Canteen { get; set; }
    }
}
