using WebAPIRestaurant.Infrastructure.Persistence;
using WebAPIRestaurant.Infrastructure.Identity;
using WebAPIRestaurant.Core.Application;
using Microsoft.AspNetCore.Identity;
using WebAPIRestaurant.Infrastructure.Identity.Entities;
using WebAPIRestaurant.Infrastructure.Identity.Seeds;
using WebAPIRestaurant.WebAPI.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersionExtension();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
        await DefaultAdministratorUser.SeedAsync(userManager, roleManager);
        await DefaultWaiterUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {

    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.AddSwaggerExtension();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
