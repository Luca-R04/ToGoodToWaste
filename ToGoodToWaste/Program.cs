using Core.DomainServices;
using DomainServices;
using DomainServices.Rules;
using Infrastructure.TGTW_EF.Data;
using Infrastructure.TGTW_EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IPackageRules, PackageRules>();
builder.Services.AddDbContext<ToGoodToWasteDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ToGoodToWasteConnectionString"])
        .EnableSensitiveDataLogging(true);
});
builder.Services.AddDbContext<SecurityDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SecurityConnectionString"])
        .EnableSensitiveDataLogging(true);
})
.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<SecurityDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<SeedData>();
builder.Services.AddScoped<SecuritySeedData>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

    options.LoginPath = "/Account/Index";
    options.AccessDeniedPath = "/Account/Index";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("Employee"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    await SeedDatabaseAsync();
}
else
{
    // Only in dev env seed with dummy data -->
    await SeedDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.Run();

// Functionality to seed the db with dummy data -->
async Task SeedDatabaseAsync()
{
    using var scope = app.Services.CreateScope();
    var dbSeeder = scope.ServiceProvider.GetRequiredService<SeedData>();
    await dbSeeder.EnsurePopulated(false);

    var dbIdentitySeeder = scope.ServiceProvider.GetRequiredService<SecuritySeedData>();
    await dbIdentitySeeder.EnsurePopulated(false);
}