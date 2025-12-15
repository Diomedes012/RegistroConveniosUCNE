using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistroConvenioUCNE.Data;
using RegistroConvenioUCNE.Models;

namespace RegistroConvenioUCNE.Data;

public static class DataSeeder
{
    public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
    {
        // 1. Obtenemos los servicios necesarios
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>(); // Inyectamos el DbContext

        // 2. Creación de Roles
        string[] roleNames = { "Decanato", "Digitador" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // 3. Creación del Usuario DECANATO y su Responsable asociado
        var decanatoEmail = "decanato@ucne.edu.do";
        var decanatoUser = await userManager.FindByEmailAsync(decanatoEmail);

        if (decanatoUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = decanatoEmail,
                Email = decanatoEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "DecanatoUCNE2025!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Decanato");
            }
        }

        // --- Insertar datos en la tabla Responsables para Decanato ---
        // Verificamos si ya existe un responsable con este email para no duplicar
        if (!await context.Responsable.AnyAsync(r => r.Email == decanatoEmail))
        {
            var responsableDecanato = new Responsable
            {
                Nombre = "Director de Decanato", // Dato propio
                Cargo = "Decano de Sistemas",    // Dato propio
                Telefono = "809-588-3505",       // Dato propio
                Email = decanatoEmail,           // Conexión con Identity
                Departamento = "Decanato de Ingeniería"
            };
            context.Responsable.Add(responsableDecanato);
        }

        // 4. Creación del Usuario DIGITADOR y su Responsable asociado
        var digitadorEmail = "digitador@ucne.edu.do";
        var digitadorUser = await userManager.FindByEmailAsync(digitadorEmail);

        if (digitadorUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = digitadorEmail,
                Email = digitadorEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "DigitadorUCNE2025!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Digitador");
            }
        }

        // --- Insertar datos en la tabla Responsables para Digitador ---
        if (!await context.Responsable.AnyAsync(r => r.Email == digitadorEmail))
        {
            var responsableDigitador = new Responsable
            {
                Nombre = "Juan Pérez",           // Dato propio
                Cargo = "Asistente Administrativo", // Dato propio
                Telefono = "809-588-3505 Ext 123",
                Email = digitadorEmail,          // Conexión con Identity
                Departamento = "Registro y Control"
            };
            context.Responsable.Add(responsableDigitador);
        }

        // 5. Guardar los cambios en la tabla Responsables
        await context.SaveChangesAsync();
    }
}