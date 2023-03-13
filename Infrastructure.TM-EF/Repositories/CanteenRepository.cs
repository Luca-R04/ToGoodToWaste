using Core.Domain;
using Core.DomainServices;
using Infrastructure.TGTW_EF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.TGTW_EF.Repositories
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly ToGoodToWasteDbContext _dbContext;

        public CanteenRepository(ToGoodToWasteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Canteen> Canteens => _dbContext.Canteens
            .Include("Employee")
            .Include("Location")
            .ToList();

        public Canteen? CreateCanteen(Canteen canteen)
        {
            _dbContext.Canteens.Add(canteen);
            _dbContext.SaveChanges();

            return canteen;
        }

        public Canteen? ReadCanteen(int canteenId)
        {
            return _dbContext.Canteens.FirstOrDefault(c => c.CanteenId == canteenId);
        }

        public Canteen? UpdateCanteen(Canteen canteen)
        {
            var entityToUpdate= _dbContext.Canteens.FirstOrDefault(c => c.CanteenId == canteen.CanteenId);
            if (entityToUpdate != null)
            {
                entityToUpdate.CanteenId = canteen.CanteenId;
                entityToUpdate.City = canteen.City;
                entityToUpdate.Location = canteen.Location;
                entityToUpdate.IsWarm = canteen.IsWarm;
                entityToUpdate.Packages = canteen.Packages;
                entityToUpdate.Employees = canteen.Employees;

                _dbContext.SaveChanges();
            }

            return entityToUpdate;
        }
        public Canteen? DeleteCanteen(Canteen canteen)
        {
            var entityToRemove = _dbContext.Canteens.FirstOrDefault(c => c.CanteenId == canteen.CanteenId);
            if (entityToRemove != null)
            {
                _dbContext.Canteens.Remove(entityToRemove);
                _dbContext.SaveChanges();
            }

            return entityToRemove;
        }
    }
}
