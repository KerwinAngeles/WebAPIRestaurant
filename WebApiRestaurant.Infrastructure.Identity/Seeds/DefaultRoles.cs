using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Enums;
using WebAPIRestaurant.Infrastructure.Identity.Entities;

namespace WebAPIRestaurant.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdministrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Waiter.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));

        }
    }
}
