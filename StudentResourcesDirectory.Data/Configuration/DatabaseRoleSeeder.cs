using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResourcesDirectory.Data.Configuration
{
    public class DatabaseRoleSeeder
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = new[] { "Admin", "User" };

            foreach (string role in roles)
            {
                bool roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    var result = roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();

                    if (!result.Succeeded)
                    {
                        throw new ArgumentException($"There was an error seeding this role: {role}");
                    }
                }
            }
        }
    }
}