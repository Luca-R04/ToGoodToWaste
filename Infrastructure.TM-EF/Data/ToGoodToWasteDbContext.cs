using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TGTW_EF.Data;

public class ToGoodToWasteDbContext : DbContext
{
    public ToGoodToWasteDbContext(DbContextOptions<ToGoodToWasteDbContext> options) : base(options)
    {

    }

    public DbSet<Canteen> Canteens { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Student> Students { get; set; }
}
