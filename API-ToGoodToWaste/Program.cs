using Core.DomainServices;
using DomainServices.Rules;
using DomainServices;
using Infrastructure.TGTW_EF.Data;
using Infrastructure.TGTW_EF.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using API_ToGoodToWaste.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IPackageRules, PackageRules>();
builder.Services.AddDbContext<ToGoodToWasteDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ToGoodToWasteConnectionString"])
        .EnableSensitiveDataLogging(true);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQLServer()
    .AddQueryType<PackageQuery>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
