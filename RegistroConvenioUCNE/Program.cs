using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistroConvenioUCNE.Components;
using RegistroConvenioUCNE.Components.Account;
using RegistroConvenioUCNE.Data;
using RegistroConvenioUCNE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("ConStr") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

if (Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    var dbPath = Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "", "site", "wwwroot", "ConveniosDB.db");
    connectionString = $"Data Source={dbPath}";
}

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<ConveniosService>();
builder.Services.AddScoped<InstitucionesService>();
builder.Services.AddScoped<ResponsablesService>();
builder.Services.AddScoped<ActividadesService>();
builder.Services.AddScoped<NotificacionesService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();

        // 1. OBLIGATORIO: Primero creamos las tablas
        Console.WriteLine("Aplicando Migraciones...");
        db.Database.Migrate();
        Console.WriteLine("Migraciones aplicadas correctamente.");

        // 2. OPCIONAL: Ahora que las tablas existen, creamos los usuarios
        Console.WriteLine("Sembrando datos...");
        await DataSeeder.SeedRolesAndUsers(services);
        Console.WriteLine("Datos sembrados.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al iniciar la Base de Datos.");
    }
}

app.Run();