using Core.Domain;
using Core.DomainServices;
using Infrastructure.TGTW_EF.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TGTW_EF.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ToGoodToWasteDbContext _dbContext;
        public IEnumerable<Location> Locations => _dbContext.Locations.ToList();

        public LocationRepository(ToGoodToWasteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Location? CreateLocation(Location location)
        {
            _dbContext.Locations.Add(location);
            _dbContext.SaveChanges();

            return location;
        }

        public Location? ReadLocation(int locationId)
        {
            return _dbContext.Locations.FirstOrDefault(c => c.LocationId == locationId);
        }

        public Location? UpdateLocation(Location location)
        {
            var entityToUpdate = _dbContext.Locations.FirstOrDefault(c => c.LocationId == location.LocationId);
            if (entityToUpdate != null)
            {
                entityToUpdate.LocationId = location.LocationId;
                entityToUpdate.Building = location.Building;
                entityToUpdate.Image = location.Image;

                _dbContext.SaveChanges();
            }

            return entityToUpdate;
        }
        public Location? DeleteLocation(Location location)
        {
            var entityToRemove = _dbContext.Locations.FirstOrDefault(c => c.LocationId == location.LocationId);
            if (entityToRemove != null)
            {
                _dbContext.Locations.Remove(entityToRemove);
                _dbContext.SaveChanges();
            }

            return entityToRemove;
        }

        public int GetHighestId(int locationId)
        {
            return Locations.Max(x => x.LocationId);
        }
    }
}
