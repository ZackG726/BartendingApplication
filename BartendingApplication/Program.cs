using BartendingApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews(); // Adds MVC services to the DI container

// Configure the DbContext with a connection string
builder.Services.AddDbContext<BartenderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BartenderDbContext")));

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Index"; // Redirect to login page if not authenticated
        options.LogoutPath = "/Account/Logout"; // Path for logout
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session timeout duration
        options.SlidingExpiration = true; // Reset the expiration time on each request
    });

// Note: Remove session services if not needed
// builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Show error page for non-development environments
    app.UseHsts();  // Enforces HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
app.UseStaticFiles();        // Serve static files (e.g., CSS, JS, images)

app.UseRouting();            // Enable routing

// Use authentication and authorization middleware
app.UseAuthentication();   // Enables authentication middleware
app.UseAuthorization();    // Enables authorization middleware

// Define the default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");  // Sets the default route to Account controller and Index action

app.Run();  // Runs the application
