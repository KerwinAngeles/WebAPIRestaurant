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
    public static class DefaultAdministratorUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "administratoruser";
            defaultUser.Email = "administratoruser@gmail.com";
            defaultUser.Name = "Leonardo";
            defaultUser.LastName = "Angeles";
            defaultUser.PhoneNumber = "8098829933";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Administrator.ToString());
                }
            }
        }
    }
}
