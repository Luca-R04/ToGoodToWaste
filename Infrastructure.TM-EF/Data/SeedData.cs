using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Core.Domain;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Infrastructure.TGTW_EF.Data;
public class SeedData : ISeedData
{
    private ToGoodToWasteDbContext _context;
    private ILogger<SeedData> _logger;

    public SeedData(ToGoodToWasteDbContext context, ILogger<SeedData> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task EnsurePopulated(bool dropExisting = false)
    {
        if (dropExisting)
        {
            //Check if database is empty
            _context.Database.EnsureDeleted();
        }
        _context.Database.Migrate();
        if (_context.Canteens?.Count() == 0 && _context.Employees?.Count() == 0 && _context.Locations?.Count() == 0
            && _context.Packages?.Count() == 0 && _context.Products?.Count() == 0 && _context.Students?.Count() == 0)
        {
            _logger.LogInformation("Preparing to seed database");

            //Create data to be inserted
            var Location1 = new Location { Building = "LD", Image = "https://www.bouwplus.nl/image/4211-26/1440x1440/Ck2wOxkIuHNrkyycuCoCVTPL25ZCVTSLhOTGaCy6NEMrfg2jj5ECVTIS-15-11_1145-Rienks-Avans-Breda.jpg", };
            var Location2 = new Location { Building = "HB", Image = "https://www.surfspot.nl/media/wysiwyg/nieuw-design/avans-hogeschool-1.jpg", };
            var Location3 = new Location { Building = "CH", Image = "https://assets.citynavigator.nl/thumb/IcVPkdVV9QMPcPUUiqq8Q8Ekjb3sA8J6geBBzGIWi_s/resizing_type:fit/width:1280/height:0/gravity:sm/enlarge:0/aHR0cHM6Ly9hc3NldHMuY2l0eW5hdmlnYXRvci5ubC9rdW1hLXRpbGJ1cmctc3R1ZGVudC91cGxvYWRzL21lZGlhLzVjNjFkOGRmYjQ0ZTYvYXZhbnMwMS5qcGc.jpg", };
            var Location4 = new Location { Building = "OB", Image = "https://www.deptagency.com/wp-content/uploads/2021/06/Avans-open-dag-2.jpg", };


            var Canteen1 = new Canteen
            {
                City = "Breda",
                Location = Location1,
                IsWarm = true,
                Packages = null,
                Employees = null,
            };
            var Canteen2 = new Canteen
            {
                City = "Breda",
                Location = Location2,
                IsWarm = true,
                Packages = null,
                Employees = null,
            };
            var Canteen3 = new Canteen
            {
                City = "Breda",
                Location = Location3,
                IsWarm = true,
                Packages = null,
                Employees = null,
            };
            var Canteen4 = new Canteen
            {
                City = "Breda",
                Location = Location4,
                IsWarm = true,
                Packages = null,
                Employees = null,
            };

            var Employee1 = new Employee { Name = "Hubert Fransworth", EmailAdress = "Hubert@gmail.com", Location = Location1, Canteen = Canteen1, };
            var Employee2 = new Employee { Name = "Philip J Fry", EmailAdress = "Philip@gmail.com", Location = Location1, Canteen = Canteen1, };
            var Employee3 = new Employee { Name = "John Doe", EmailAdress = "Jonh@gmail.com", Location = Location2, Canteen = Canteen2, };

            Canteen1.Employees = new List<Employee>() { Employee1, Employee2 };
            Canteen2.Employees = new List<Employee>() { Employee3 };

            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var Product3 = new Product { Name = "Pasta", HasAlcohol = false, Image = "https://assets.tmecosys.com/image/upload/t_web767x639/img/recipe/ras/Assets/658A0A74-039A-487C-A07A-CAAF61B4615D/Derivates/A230DF28-60DF-429D-ABDA-96ED64E9EE10.jpg" };
            var Product4 = new Product { Name = "Tomatensaus", HasAlcohol = false, Image = "https://koken.vtm.be/sites/koken.vtm.be/files/styles/vmmaplayer_big/public/recipe/image/istock_000002462689small.jpg?itok=9Bova4dI" };
            var Product5 = new Product { Name = "basilicum", HasAlcohol = false, Image = "https://www.ahealthylife.nl/wp-content/uploads/2018/09/voedingswaarde-basilicum.jpg" };
            var Product6 = new Product { Name = "Cola", HasAlcohol = false, Image = "https://www.nnpartners.nl/autoimg/280554/1024x768/ffffff/coca-cola-regular-frisdrank-0-33-l-blik-pak-24-x-330-milliliter-.jpg" };
            var Product7 = new Product { Name = "Tosti", HasAlcohol = false, Image = "https://soulfood.nl/wp-content/uploads/2021/01/tosti.jpg" };
            var Product8 = new Product { Name = "Bier", HasAlcohol = true, Image = "https://www.plus.nl/INTERSHOP/static/WFS/PLUS-Site/-/PLUS/nl_NL/product/L/846671.png" };

            var ProductList = new List<Product>() { Product1, Product2 };
            var ProductList2 = new List<Product>() { Product3, Product4, Product5 };
            var ProductList3 = new List<Product>() { Product6, Product7 };
            var ProductList4 = new List<Product>() { Product8 };
            var ProductList5 = new List<Product>() { Product3, Product4, Product7 };
            var ProductList6 = new List<Product>() { Product6, Product8 };
            var ProductList7 = new List<Product>() { Product1, Product2, Product6, Product7 };
            var ProductList8 = new List<Product>() { Product2, Product3 };

            var Package1 = new Package
            {
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = Canteen1,
                PickupTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = false,
                Price = 13.95,
                Category = Categories.Brood,
                ForStudent = null,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };
            var Package2 = new Package
            {
                Name = "Pasta box",
                Products = ProductList2,
                City = "Breda",
                Canteen = Canteen1,
                PickupTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = false,
                Price = 12.99,
                Category = Categories.Warm,
                ForStudent = null,
                Image = "https://www.24kitchen.nl/files/styles/960h_960w/public/2018-09/Screen%20Shot%202018-09-11%20at%2016.46.22.webp?itok=TSwb6Nvz"
            };
            var Package3 = new Package
            {
                Name = "Kratje bier",
                Products = ProductList4,
                City = "Breda",
                Canteen = Canteen1,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ForAdult = true,
                Price = 12.95,
                Category = Categories.Drank,
                ForStudent = null,
                Image = "https://cdn.shopify.com/s/files/1/0563/8864/9168/products/bierkrat_250x250@2x.png?v=1625679498"
            };
            var Package4 = new Package
            {
                Name = "Tosti met cola",
                Products = ProductList3,
                City = "Breda",
                Canteen = Canteen1,
                PickupTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(2),
                ForAdult = false,
                Price = 10.00,
                Category = Categories.Brood,
                ForStudent = null,
                Image = "https://media-cdn.tripadvisor.com/media/photo-s/12/4a/b1/64/grilled-cheese-kings.jpg"
            };
            var Package5 = new Package
            {
                Name = "Pasta & Tosti box",
                Products = ProductList5,
                City = "Breda",
                Canteen = Canteen2,
                PickupTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(2),
                ForAdult = false,
                Price = 15.89,
                Category = Categories.Warm,
                ForStudent = null,
                Image = "https://www.24kitchen.nl/files/styles/960h_960w/public/2018-09/Screen%20Shot%202018-09-11%20at%2016.46.22.webp?itok=TSwb6Nvz"
            };
            var Package6 = new Package
            {
                Name = "Cola en bier",
                Products = ProductList6,
                City = "Breda",
                Canteen = Canteen2,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ForAdult = true,
                Price = 8.95,
                Category = Categories.Drank,
                ForStudent = null,
                Image = "https://cdn.shopify.com/s/files/1/0563/8864/9168/products/bierkrat_250x250@2x.png?v=1625679498"
            };
            var Package7 = new Package
            {
                Name = "Lunch pakket",
                Products = ProductList7,
                City = "Breda",
                Canteen = Canteen3,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ForAdult = false,
                Price = 10.00,
                Category = Categories.Brood,
                ForStudent = null,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };
            var Package8 = new Package
            {
                Name = "pasta met worst",
                Products = ProductList8,
                City = "Breda",
                Canteen = Canteen4,
                PickupTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(2),
                ForAdult = false,
                Price = 9.95,
                Category = Categories.Warm,
                ForStudent = null,
                Image = "https://www.24kitchen.nl/files/styles/960h_960w/public/2018-09/Screen%20Shot%202018-09-11%20at%2016.46.22.webp?itok=TSwb6Nvz"
            };

            Canteen1.Packages = new List<Package>() { Package1, Package2, Package3, Package4 };
            Canteen2.Packages = new List<Package>() { Package5, Package6 };
            Canteen3.Packages = new List<Package>() { Package7 };
            Canteen4.Packages = new List<Package>() { Package8 };

            var student1 = new Student { Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = Location1 };
            var student2 = new Student { Name = "Laurens Weterings", BirthDate = DateTime.Today.AddYears(-19), StudentNumber = "7654321", EmailAdress = "Laurens@gmail.com", City = "Breda", PhoneNumber = "06 11121314", Location = Location1 };
            var student3 = new Student { Name = "Martijn Schermers", BirthDate = DateTime.Today.AddYears(-19), StudentNumber = "1726354", EmailAdress = "Martijn@gmail.com", City = "Breda", PhoneNumber = "06 10111213", Location = Location2 };

            //Insert into the database
            _context.Locations.AddRange(new[] { Location1, Location2, Location3, Location4, });

            _context.Products.AddRange(new[] { Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, });

            _context.Packages.AddRange(new[] { Package1, Package2, Package3, Package4, Package5, Package6, Package7, Package8, });

            _context.Employees.AddRange(new[] { Employee1, Employee2, Employee3, });

            _context.Canteens.AddRange(new[] { Canteen1, Canteen2, Canteen3, Canteen4, });

            _context.Students.AddRange(new[] { student1, student2, student3, });

            _context.SaveChanges();
            _logger.LogInformation("Database seeded");
        }
        else
        {
            _logger.LogInformation("Database not seeded");
        }
    }
}
