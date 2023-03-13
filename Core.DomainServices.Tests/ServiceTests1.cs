using Core.Domain;
using Core.DomainServices;
using DomainServices.Rules;
using Moq;

namespace Core.DomainServices.Tests
{
    public class ServiceTests1
    {
        [Fact]
        public void UpdatePackageHasReservation()
        {
            //arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };
            var student = new Student { StudentId = 1, Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = location };

            var package = new Package
            {
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = false,
                Price = 13.95,
                Category = Categories.Brood,
                ForStudent = student,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };

            var packageRules = new PackageRules(packageRepoMock.Object);

            //act
            Action act = () => packageRules.UpdatePackage(package);


            //assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("Package already reserved", exception.Message);
        }

        [Fact]
        public void UpdatePackageHasNoPrice()
        {
            //arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };
            var student = new Student { StudentId = 1, Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = location };

            var package = new Package
            {
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = false,
                Price = 0,
                Category = Categories.Brood,
                ForStudent = null,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };

            var packageRules = new PackageRules(packageRepoMock.Object);

            //act
            Action act = () => packageRules.UpdatePackage(package);


            //assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("PriceNot", exception.Message);
        }

        [Fact]
        public void ReservePackageNotExisting()
        {
            //arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };
            var student = new Student { StudentId = 1, Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = location };

            var packageRules = new PackageRules(packageRepoMock.Object);

            //act
            Action act = () => packageRules.ReservePackage(student, null);


            //assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("NoPackage", exception.Message);
        }

        [Fact]
        public void ReservePackageOccupied()
        {
            //arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };
            var student = new Student { StudentId = 1, Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = location };

            var package = new Package
            {
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = false,
                Price = 0,
                Category = Categories.Brood,
                ForStudent = student,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };

            var packageRules = new PackageRules(packageRepoMock.Object);

            //act
            Action act = () => packageRules.ReservePackage(student, package);


            //assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("Occupied", exception.Message);
        }

        [Fact]
        public void ReservePackageSameDayReservation()
        {
            //arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };
            var student = new Student { StudentId = 1, Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = location, ReservedPackages = new List<Package>() };

            var package = new Package
            {
                PackageId = 1,
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = false,
                Price = 0,
                Category = Categories.Brood,
                ForStudent = student,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };

            var package2 = new Package
            {
                PackageId = 2,
                Name = "Tosti met cola",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = false,
                Price = 10.00,
                Category = Categories.Warm,
                ForStudent = null,
                Image = "https://soulfood.nl/wp-content/uploads/2021/01/tosti.jpg"
            };

            student.ReservedPackages.Add(package);

            var packageRules = new PackageRules(packageRepoMock.Object);

            //act
            Action act = () => packageRules.ReservePackage(student, package2);


            //assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("SameDayExisting", exception.Message);
        }

        [Fact]
        public void ReservePackageAgeLimit()
        {
            //arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };
            var student = new Student { StudentId = 1, Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = location };

            var package = new Package
            {
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = true,
                Price = 0,
                Category = Categories.Brood,
                ForStudent = null,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };

            var packageRules = new PackageRules(packageRepoMock.Object);

            //act
            Action act = () => packageRules.ReservePackage(student, package);


            //assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("AgeLimit", exception.Message);
        }

        [Fact]
        public void DeletePackageReserved()
        {
            //arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };
            var student = new Student { StudentId = 1, Name = "Luca Rinck", BirthDate = DateTime.Today.AddYears(-17), StudentNumber = "1234567", EmailAdress = "Luca@gmail.com", City = "Breda", PhoneNumber = "06 12131415", Location = location };

            var package = new Package
            {
                PackageId = 1,
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                ForAdult = true,
                Price = 0,
                Category = Categories.Brood,
                ForStudent = student,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };

            var packageRules = new PackageRules(packageRepoMock.Object);

            //act
            Action act = () => packageRules.CheckOccupied(package);

            //assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("Occupied", exception.Message);
        }
    }
}