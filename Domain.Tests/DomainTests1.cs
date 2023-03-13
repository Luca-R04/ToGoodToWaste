using Core.Domain;

namespace Domain.Tests
{
    public class DomainTests1
    {
        [Fact]
        public void PackagePickupTimeToFarInFuture()
        {
            //arrange
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };
            var canteen = new Canteen() { CanteenId = 1, City = "Breda", IsWarm = true, Location = location };
            var Product1 = new Product { Name = "Broodje", HasAlcohol = false, Image = "https://t4.ftcdn.net/jpg/03/15/58/03/360_F_315580349_Wxwd3VixxDTM7JtjuP6Hr2iOeRwz1plO.jpg" };
            var Product2 = new Product { Name = "Unox worst", HasAlcohol = false, Image = "https://hilabel.ams3.cdn.digitaloceanspaces.com/girlscene/uploads/2021/03/knakworst.jpg" };
            var ProductList = new List<Product>() { Product1, Product2 };

            //act
            Action act = () => new Package
            {
                Name = "Broodje unox",
                Products = ProductList,
                City = "Breda",
                Canteen = canteen,
                PickupTime = DateTime.Now.AddDays(3),
                EndTime = DateTime.Now.AddDays(6),
                ForAdult = true,
                Price = 13.95,
                Category = Categories.Brood,
                ForStudent = null,
                Image = "https://www.unileverfoodsolutions.nl/dam/global-ufs/mcos/BENEFRA/calcmenu/recipes/NL-recipes/sandwiches/broodje-unox/main-header.jpg"
            };

            //assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Datum mag maximaal over 2 dagen zijn", exception.Message);
        }

        [Fact]
        public void StudentBirthDateTooLow()
        {
            //arrange
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };

            //act
            Action act = () => new Student
            {
                Name = "Luca Rinck",
                BirthDate = DateTime.Today.AddYears(-15),
                StudentNumber = "1234567",
                EmailAdress = "Luca@gmail.com",
                City = "Breda",
                PhoneNumber = "06 12131415",
                Location = location
            };

            //assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Leeftijd mag niet lager zijn dan 16", exception.Message);
        }

        [Fact]
        public void StudentBirthDateTooFarInFuture()
        {
            //arrange
            var location = new Location() { LocationId = 1, Building = "LD", Image = "test" };

            //act
            Action act = () => new Student
            {
                Name = "Luca Rinck",
                BirthDate = DateTime.Today.AddYears(1),
                StudentNumber = "1234567",
                EmailAdress = "Luca@gmail.com",
                City = "Breda",
                PhoneNumber = "06 12131415",
                Location = location
            };

            //assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Verjaardag kan niet in de toekomst zijn", exception.Message);
        }
    }
}