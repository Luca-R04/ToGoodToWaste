using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IPackageRules
    {
        public void CreatePackage(Package package);
        public void UpdatePackage(Package package);
        public void ReservePackage(Student student, Package package);
        public void DeletePackage(int Id);
    }
}
