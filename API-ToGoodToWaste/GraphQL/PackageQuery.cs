using Core.DomainServices;
using Core.Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace API_ToGoodToWaste.GraphQL
{
    public class PackageQuery
    {
        public IEnumerable<Package> GetPackages([Service] IPackageRepository packageRepository)
        {
            return packageRepository.Packages;
        }
    }
}
