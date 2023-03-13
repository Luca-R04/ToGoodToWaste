using Core.Domain;

namespace ToGoodToWaste.ViewModels
{
    public class UserPageViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Package> Packages { get; set; } = Enumerable.Empty<Package>();
        public string newLocation { get; set; }
    }
}
