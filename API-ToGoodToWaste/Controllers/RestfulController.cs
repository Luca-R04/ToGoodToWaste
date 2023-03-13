using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Domain;
using Core.DomainServices;
using System.Text.Json.Serialization;
using System.Net;
using DomainServices;

namespace API_ToGoodToWaste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestfulController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPackageRules _packageRules;

        public RestfulController(IPackageRepository packageRepository, IStudentRepository studentRepository, IPackageRules packageRules)
        {
            _packageRepository = packageRepository;
            _studentRepository = studentRepository;
            _packageRules = packageRules;
        }

        [HttpGet(Name = "GetPackages")]
        public ActionResult<List<Package>> Get()
        {
            var packages = _packageRepository.Packages;
            if (!packages.Any())
            {
                return NotFound();
            }

            return Ok(packages);
        }

        [HttpPut("{studentId}/{packageId}")]
        public ActionResult Put(int studentId, int packageId)
        {
            var student = _studentRepository.ReadStudent(studentId);
            var package = _packageRepository.ReadPackage(packageId);

            if (student == null || package == null)
            {
                return NotFound();
            }

            try
            {
                _packageRules.ReservePackage(student, package);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok();
        }
    }
}
