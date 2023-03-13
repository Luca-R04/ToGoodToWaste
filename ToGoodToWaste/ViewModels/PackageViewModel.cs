using Core.Domain;

namespace ToGoodToWaste.ViewModels
{
    public class PackageViewModel
    {
        public IEnumerable<Package> Packages { get; set; } = Enumerable.Empty<Package>();

        public Categories Category { get; set; }
    }
}
