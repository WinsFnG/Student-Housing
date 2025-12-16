using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Student_housing.Data;
using Student_housing.Models;
using Student_housing.PasswordServices;

var builder = WebApplication.CreateBuilder(args);


// Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddScoped<Student_housing.Filters.AccountGateFilter>();

// Register MVC
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<Student_housing.Filters.AccountGateFilter>(); // global filter for account gating (banned users, terms not accepted users)
});

// Register DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// Cookie Authentication 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>  // how does cookie authentication behave
    {
        options.LoginPath = "/Account/Login"; // Redirect to login page if not authenticated
        options.LogoutPath = "/Account/Logout"; // Redirect to logout page ( make logout in account)!
        options.AccessDeniedPath = "/Home/Dashboard"; // Redirect to goodbye page
        options.Cookie.Name = "StudentHousingAuthCookie";
        options.Cookie.HttpOnly = true; // since we arent using pop up for cookies js cant read it = safer (browser still can)
        options.Cookie.SameSite = SameSiteMode.Lax; // Mitigate CSRF attacks with lax policy being a good middleground
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // https if https, http if http 
    });
// Authorization policies for role-based access control
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LandlordOnly", p => p.RequireRole("Landlord"));
    options.AddPolicy("TenantOnly",p => p.RequireRole("Tenant") );
  }  );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();
    var cfg = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var landlordCfg = cfg.GetSection("SeedUsers:Landlord");

    var landlordUsername = landlordCfg["Username"];
    var landlordEmail = landlordCfg["Email"];
    var landlordPassword = landlordCfg["Password"];

    if (string.IsNullOrWhiteSpace(landlordUsername) ||
        string.IsNullOrWhiteSpace(landlordEmail) ||
        string.IsNullOrWhiteSpace(landlordPassword))
    {
        throw new Exception("SeedUsers:Landlord config is missing or invalid");
    }

    if (!db.Users.Any(u => u.Username == landlordUsername))
    {
        var hashed = PasswordHasher.HashPassword(landlordPassword);

        db.Users.Add(new User
        {
            Username = landlordUsername,
            Email = landlordEmail,
            PasswordHash = hashed,
            Role = "Landlord",
            AcceptedTerms = true,
            IsBanned = false
        });

        db.SaveChanges();
    }



    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error"); // error path
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication(); //(for cookies and sessions later on-not currently active)
    app.UseAuthorization();// put this in as well pls:)

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");

    app.Run();
}
