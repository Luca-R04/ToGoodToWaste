using Core.Domain;
using Core.DomainServices;
using Infrastructure.TGTW_EF.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TGTW_EF.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ToGoodToWasteDbContext _dbContext;
        public IEnumerable<Package> Packages => _dbContext.Packages
            .Include(package => package.Canteen)
            .ThenInclude(canteen => canteen.Location)
            .Include(package => package.Canteen)
            .ThenInclude(canteen => canteen.Employees)
            .Include(package => package.Products)
            .Include(package => package.ForStudent)
            .ToList();

        public PackageRepository(ToGoodToWasteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Package? CreatePackage(Package package)
        {
            _dbContext.Packages.Add(package);
            _dbContext.SaveChanges();

            return package;
        }

        public Package? ReadPackage(int packageId)
        {
            return Packages.FirstOrDefault(c => c.PackageId == packageId);
        }

        public Package? UpdatePackage(Package package)
        {
            var entityToUpdate = _dbContext.Packages.Include("Products").FirstOrDefault(c => c.PackageId == package.PackageId);
            if (entityToUpdate != null)
            {
                entityToUpdate!.Products!.Any(p => entityToUpdate.Products!.Remove(p));

                entityToUpdate.PackageId = package.PackageId;
                entityToUpdate.Name = package.Name;
                entityToUpdate.Products = package.Products;
                entityToUpdate.City = package.City;
                entityToUpdate.Canteen = package.Canteen;
                entityToUpdate.PickupTime = package.PickupTime;
                entityToUpdate.EndTime = package.EndTime;
                entityToUpdate.ForAdult = package.ForAdult;
                entityToUpdate.Price = package.Price;
                entityToUpdate.Category = package.Category;
                entityToUpdate.ForStudent = package.ForStudent;

                _dbContext.SaveChanges();
            }

            return entityToUpdate;
        }
        public Package? DeletePackage(Package package)
        {
            var entityToRemove = _dbContext.Packages.FirstOrDefault(c => c.PackageId == package.PackageId);
            if (entityToRemove != null)
            {
                _dbContext.Packages.Remove(entityToRemove);
                _dbContext.SaveChanges();
            }

            return entityToRemove;
        }

        public IEnumerable<Package> GetAvailablePackages(int locationId)
        {
            return Packages
                    .Where(p => p.Canteen.Location.LocationId == locationId)
                    .Where(p => p.ForStudent == null)
                    .Where(p => p.EndTime > DateTime.Now)
                    .OrderBy(p => p.PickupTime)
                    .ToList();
        }

        public IEnumerable<Package> GetOfferedPackages(int locationId)
        {
            return Packages
                    .Where(p => p.Canteen.Location.LocationId == locationId)
                    .Where(p => p.ForStudent == null)
                    .OrderBy(p => p.PickupTime)
                    .ToList();
        }

        public IEnumerable<Package> GetOtherPackages(int locationId)
        {
            return Packages
                    .Where(p => p.Canteen.Location.LocationId != locationId)
                    .ToList();
        }

        public IEnumerable<Package> GetReservedPackages(int locationId)
        {
            return Packages
                    .Where(p => p.Canteen.Location.LocationId == locationId)
                    .Where(p => p.ForStudent != null)
                    .ToList();
        }

        public IEnumerable<Package> GetUserPackages(string email)
        {
            return Packages
                 .Where(package => package.ForStudent != null)
                 .Where(package => package.ForStudent.EmailAdress.Equals(email))
                 .ToList();
        }
    }
}
