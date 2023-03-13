using Core.Domain;

namespace Core.DomainServices
{
    public interface IPackageRepository
    {
        IEnumerable<Package> Packages { get; }

        Package? CreatePackage(Package package);
        Package? ReadPackage(int packageId);
        Package? UpdatePackage(Package package);
        Package? DeletePackage(Package package);
        IEnumerable<Package>? GetAvailablePackages(int locationId);
        IEnumerable<Package>? GetOfferedPackages(int locationId);
        IEnumerable<Package>? GetOtherPackages(int locationId);
        IEnumerable<Package>? GetReservedPackages(int locationId);
        IEnumerable<Package>? GetUserPackages(string email);
    }
}