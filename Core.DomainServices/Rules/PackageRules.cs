using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;

namespace DomainServices.Rules
{
    public class PackageRules : IPackageRules
    {
        private readonly IPackageRepository _packageRepository;

        public PackageRules(IPackageRepository PackageRepository)
        {
            _packageRepository = PackageRepository;
        }

        public void CreatePackage(Package package)
        {
            package = HasAlcohol(package);

            _packageRepository.CreatePackage(package);
        }

        public void UpdatePackage(Package package)
        {
            package = HasAlcohol(package);
            if (package.ForStudent != null)
            {
                throw new Exception("Package already reserved");
            }
            if (package.Price == 0)
            {
                throw new Exception("PriceNot");
            }

            _packageRepository.UpdatePackage(package);
        }

        public void ReservePackage(Student student, Package package)
        {
            if (package == null)
            {
                throw new Exception("NoPackage");
            }
            if (package.ForStudent != null)
            {
                throw new Exception("Occupied");
            }
            if (CheckSameDay(student, package))
            {
                throw new Exception("SameDayExisting");
            }
            if (package.ForAdult && student.BirthDate.AddYears(-18) < package.PickupTime.AddYears(-18))
            {
                throw new Exception("AgeLimit");
            }

            package.ForStudent = student;
            _packageRepository.UpdatePackage(package);
        }

        public void DeletePackage(int Id)
        {
            var package = _packageRepository.ReadPackage(Id);
            CheckOccupied(package);
        }

        public void CheckOccupied(Package package)
        {
            if (package.ForStudent != null)
            {
                throw new Exception("Occupied");
            }

            _packageRepository.DeletePackage(package);
        }

        private bool CheckSameDay(Student student, Package package)
        {
            if (student.ReservedPackages != null)
            {
                foreach (Package item in student.ReservedPackages)
                {
                    if (item.PickupTime.Date == package.PickupTime.Date)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Package HasAlcohol(Package package)
        {
            foreach (var product in package.Products)
            {
                if (product.HasAlcohol)
                {
                    package.ForAdult = true;
                    return package;
                }
            }
            return package;
        }
    }
}
