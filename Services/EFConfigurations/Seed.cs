using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFConfigurations
{
    public class Seed
    {
        public static async Task SeedData(DataBaseContext dataBase,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                User user = new()
                {
                    DisplayName = "Rahim Lotfi Nemati",
                    Email = "sinuhe_1368@yahoo.com",
                    UserName = "sinuhe_1368",
                };

                var result=await userManager.CreateAsync(user, "799368cr");

                if (!roleManager.Roles.Any() && result.Succeeded)
                {
                    List<Role> basicRoles = new List<Role>()
                    {
                        new Role() { Name = "Programer" },
                        new Role() { Name = "Owner" },
                        new Role() { Name = "Admin" },
                        new Role() { Name = "GeneralUser" }
                    };

                    foreach (var role in basicRoles)
                    {
                        await roleManager.CreateAsync(role);
                    }


                    var programerRole = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "Programer");

                    await userManager.AddToRoleAsync(user, programerRole.Name);
                }
                
            }
        }
    }
}
