using DAL;
using DAL.Contracts.App;
using DAL.Seeding;

using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ResevationSysDb") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// register our UOW with scoped lifecycle
builder.Services.AddScoped<IAppUOW, AppUOW>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});

builder.Services.AddControllersWithViews();


var app = builder.Build();

// setup database stuff
SetupAppData(app, app.Environment, app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment environment, IConfiguration configuration)
{
    
    using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    
    if(context == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize DB context.");
    }

    using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
    using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
    if (userManager == null || roleManager == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize UserManager or RoleManager");
    }

    var logger = serviceScope.ServiceProvider.GetService<ILogger<IApplicationBuilder>>();
    if (logger == null)
    {
        throw new ApplicationException("Problem in services. Can't initialize logger.");
    }

    if (context.Database.ProviderName!.Contains("InMemory"))
    {
        return;
    }
    // TODO: Wait for db connection
    
    // drop
    if (configuration.GetValue<bool>("DataInit:DropDatabase"))
    {
        logger.LogWarning("Dropping database");
        AppDataInit.DropDatabase(context);
    }
    // migrate
    if (configuration.GetValue<bool>("DataInit:MigrateDatabase"))
    {
        logger.LogInformation("Migrating database");
        AppDataInit.MigrateDatabase(context);
    }
    // seed identity
    if (configuration.GetValue<bool>("DataInit:SeedIdentity"))
    {
        logger.LogInformation("Seeding identity");
        AppDataInit.SeedIdentity(userManager, roleManager);
    }
    // seed application data
    if (configuration.GetValue<bool>("DataInit:SeedData"))
    {
        logger.LogWarning("Seed app data");
        AppDataInit.SeedAppData(context);
    }
}