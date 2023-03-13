using Core.Domain;
using Core.DomainServices;
using DomainServices;
using DomainServices.Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using ToGoodToWaste.ViewModels;

namespace ToGoodToWaste.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ICanteenRepository _canteenRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPackageRules _packageRules;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(ILocationRepository locationRepository, IPackageRepository PackageRepository,
            ICanteenRepository canteenRepository, IStudentRepository studentRepository, IPackageRules packageRules, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _packageRepository = PackageRepository;
            _canteenRepository = canteenRepository;
            _locationRepository = locationRepository;
            _studentRepository = studentRepository;
            _packageRules = packageRules;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        public ViewResult Location()
        {
            var locations = _locationRepository.Locations;

            var viewModel = new LocationViewModel
            {
                Locations = locations,
            };

            return View(viewModel);
        }

        [Authorize]
        public ViewResult Package(int Id)
        {
            var maxId = _locationRepository.GetHighestId(Id);
            if (maxId < Id || Id == 0)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var student = _studentRepository.GetStudentByEmail(email);

                Id = student.Location.LocationId;
            }

            var packages = _packageRepository.GetAvailablePackages(Id);

            var viewModel = new PackageViewModel
            {
                Packages = packages,
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Package(PackageViewModel viewModel, int Id)
        {
            var maxId = _locationRepository.GetHighestId(Id);
            if (maxId < Id || Id == 0)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var student = _studentRepository.GetStudentByEmail(email);

                Id = student.Location.LocationId;
            }

            var packages = _packageRepository.GetAvailablePackages(Id);

            if (viewModel.Category == 0)
            {
                viewModel.Packages = packages;
                return View(viewModel);
            }

            viewModel.Packages = packages
                .Where(p => p.Category == viewModel.Category)
                .ToList();

            return View(viewModel);
        }

        [Authorize]
        public ViewResult PackageDetail(int Id)
        {
            var package = _packageRepository.ReadPackage(Id);
            return View(package);
        }

        [Authorize]
        public ViewResult Confirmation(int Id)
        {
            var package = _packageRepository.ReadPackage(Id);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var student = _studentRepository.GetStudentByEmail(email);

            try
            {
                _packageRules.ReservePackage(student, package);
            }
            catch (Exception ex)
            {
                ViewBag.Succes = ex.Message;
                return View();
            }

            ViewBag.Succes = "Succes";
            return View();
        }

        [Authorize]
        public ViewResult UserPage()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var packages = _packageRepository.GetUserPackages(email);

            var viewModel = new UserPageViewModel
            {
                Student = _studentRepository.GetStudentByEmail(email),
                Packages = packages,
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UserPage(UserPageViewModel viewModel)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var student = _studentRepository.GetStudentByEmail(email);

            var locations = _locationRepository.Locations;

            foreach (var location in locations)
            {
                if (viewModel.newLocation.Equals(location.Building))
                {
                    student.City = "Breda";
                    student.Location = location;
                }
            }

            _studentRepository.UpdateStudent(student);

            return Redirect("Location");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user =
                    await _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user,
                        loginVM.Password, false, false)).Succeeded)
                    {
                        var claims = await _userManager.GetClaimsAsync(user);

                        var isAdmin = claims.Any(claim => claim.Type == "Employee" && claim.Value == "true");

                        if (isAdmin)
                        {
                            return Redirect("/Admin/Select");
                        }

                        return Redirect("Location");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View("Index");
        }

        public async Task<RedirectResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("Index");
        }
    }
}