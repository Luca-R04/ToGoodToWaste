using Core.Domain;

namespace Core.DomainServices
{
    public interface ILocationRepository
    {
        IEnumerable<Location> Locations { get; }

        Location? CreateLocation(Location location);
        Location? ReadLocation(int locationId);
        Location? UpdateLocation(Location location);
        Location? DeleteLocation(Location location);
        int GetHighestId(int locationId);
    }
}