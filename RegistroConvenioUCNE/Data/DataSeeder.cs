using Microsoft.AspNetCore.Identity;
using RegistroConvenioUCNE.Data;
using RegistroConvenioUCNE.Models;

namespace RegistroConvenioUCNE.Data;

public static class DataSeeder
{
    public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Decanato", "Digitador" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var decanatoUser = await userManager.FindByEmailAsync("decanato@ucne.edu.do");
        if (decanatoUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = "decanato@ucne.edu.do",
                Email = "decanato@ucne.edu.do",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "DecanatoUCNE2025!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Decanato");
            }
        }

        var digitadorUser = await userManager.FindByEmailAsync("digitador@ucne.edu.do");
        if (digitadorUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = "digitador@ucne.edu.do",
                Email = "digitador@ucne.edu.do",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "DigitadorUCNE2025!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Digitador");
            }
        }
    }
}