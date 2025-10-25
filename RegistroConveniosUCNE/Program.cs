using GestionConveniosUCNE.Models;
using GestionConveniosUCNE.Services;
using Microsoft.EntityFrameworkCore;
using RegistroConveniosUCNE.Components;

namespace RegistroConveniosUCNE;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ConStr");

        builder.Services.AddDbContextFactory<Contexto>(options => options.UseSqlite(connectionString));


        builder.Services.AddScoped<ConveniosService>();
        builder.Services.AddScoped<InstitucionesService>();
        builder.Services.AddScoped<ResponsablesService>();
        builder.Services.AddScoped<ActividadesService>();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
