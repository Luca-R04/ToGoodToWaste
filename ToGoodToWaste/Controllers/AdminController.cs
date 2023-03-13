using Core.DomainServices;
using Infrastructure.TGTW_EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToGoodToWaste.ViewModels;
using Core.Domain;
using DomainServices;

namespace ToGoodToWaste.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPackageRepository _packageRepository;
        public readonly IEmployeeRepository _employeeRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPackageRules _packageRules;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminController(IEmployeeRepository employeeRepository, IPackageRepository packageRepository, ILocationRepository locationRepository,
            IProductRepository productRepository, IPackageRules packageRules, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _packageRepository = packageRepository;
            _employeeRepository = employeeRepository;
            _locationRepository = locationRepository;
            _productRepository = productRepository;
            _packageRules = packageRules;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Select()
        {
            return View();
        }

        [Authorize(Policy = "EmployeeOnly")]
        public ViewResult AdminPage()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = _employeeRepository.GetEmployeeByEmail(email);
            return View(user);
        }

        [Authorize(Policy = "EmployeeOnly")]
        public ViewResult PackageForm()
        {
            var viewmodel = new CreatePackageViewModel
            {
                minTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm"),
                maxTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-ddThh:mm"),

                checkBoxes = new List<CheckBoxOption>(),
            };

            foreach (var product in _productRepository.Products)
            {
                viewmodel.checkBoxes.Add(new CheckBoxOption()
                {
                    IsChecked = false,
                    Description = product.Name,
                    product = product,
                });
            }

            return View(viewmodel);
        }

        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult CreatePackage(CreatePackageViewModel packageViewModel)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var employee = _employeeRepository.GetEmployeeByEmail(email);

            var package = packageViewModel.package;

            package.Canteen = employee.Canteen;
            package.City = employee.Canteen.City;

            var products = new List<Product>();
            foreach (var item in packageViewModel.addedProducts)
            {
                products.Add(_productRepository.ReadProduct(item));
            }
            package.Products = products;

            _packageRules.CreatePackage(package);

            return Redirect("Offering");

        }

        [Authorize(Policy = "EmployeeOnly")]
        public ViewResult Offering()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var employee = _employeeRepository.GetEmployeeByEmail(email);
            int Id = employee.Location.LocationId;

            var packages = _packageRepository.GetOfferedPackages(Id);
            var viewModel = new PackageViewModel
            {
                Packages = packages,
            };

            ViewBag.IsOther = false;
            return View(viewModel);
        }

        [Authorize(Policy = "EmployeeOnly")]
        public ViewResult OtherOffering()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var employee = _employeeRepository.GetEmployeeByEmail(email);
            int Id = employee.Location.LocationId;

            var packages = _packageRepository.GetOtherPackages(Id);
            var viewModel = new PackageViewModel
            {
                Packages = packages,
            };

            ViewBag.IsOther = true;
            return View("Offering", viewModel);
        }

        [Authorize(Policy = "EmployeeOnly")]
        public ViewResult Reservations()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var employee = _employeeRepository.GetEmployeeByEmail(email);
            int Id = employee.Location.LocationId;

            var packages = _packageRepository.GetReservedPackages(Id);
            var viewModel = new PackageViewModel
            {
                Packages = packages,
            };

            return View(viewModel);
        }

        [Authorize(Policy = "EmployeeOnly")]
        public ViewResult EditPackage(int Id)
        {
            var editPackage = _packageRepository.ReadPackage(Id);

            var viewmodel = new CreatePackageViewModel
            {
                package = editPackage,

                minTime = DateTime.Today.ToString("yyyy-MM-dd"),
                maxTime = DateTime.Today.AddDays(2).ToString("yyyy-MM-dd"),

                checkBoxes = new List<CheckBoxOption>(),
            };

            foreach (var product in _productRepository.Products)
            {
                if (editPackage.Products.Contains(product))
                {
                    viewmodel.checkBoxes.Add(new CheckBoxOption()
                    {
                        IsChecked = true,
                        Description = product.Name,
                        product = product,
                    });
                }
                else
                {
                    viewmodel.checkBoxes.Add(new CheckBoxOption()
                    {
                        IsChecked = false,
                        Description = product.Name,
                        product = product,
                    });
                }
            }

            return View(viewmodel);
        }

        [HttpPost]
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult EditPackage(CreatePackageViewModel viewModel)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var employee = _employeeRepository.GetEmployeeByEmail(email);

            var package = viewModel.package;

            package.Canteen = employee.Canteen;
            package.City = employee.Canteen.City;

            var products = new List<Product>();
            foreach (var item in viewModel.addedProducts)
            {
                products.Add(_productRepository.ReadProduct(item));
            }
            package.Products = products;

            try
            {
                _packageRules.UpdatePackage(package);
            }
            catch (Exception ex)
            {
                ViewBag.Succes = ex.Message;
                return View("AdminConfirmation");
            }

            ViewBag.Succes = "Edited";
            return View("AdminConfirmation");
        }

        [Authorize(Policy = "EmployeeOnly")]
        public ViewResult DeletePackage(int Id)
        {
            try
            {
                _packageRules.DeletePackage(Id);
            }
            catch (Exception ex)
            {
                ViewBag.Succes = ex.Message;
                return View("AdminConfirmation");
            }

            ViewBag.Succes = "Deleted";
            return View("AdminConfirmation");
        }
    }
}
