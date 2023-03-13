using Core.Domain;

namespace ToGoodToWaste.ViewModels
{
    public class LocationViewModel
    {
        public IEnumerable<Location> Locations { get; set; } = Enumerable.Empty<Location>();
    }
}
