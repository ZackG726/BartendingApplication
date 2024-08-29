using BartendingApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace BartendingApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly BartenderDbContext _context;

        // Constructor to initialize the BartenderDbContext
        public AccountController(BartenderDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        // Displays the login page
        public IActionResult Index()
        {
            return View();
        }

        // POST: Account/Login
        // Handles the login form submission
        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            // Validate user credentials
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Create the user claims for authentication
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Redirect based on user role
                if (user.Role == "Customer")
                {
                    return RedirectToAction("Index", "OrderDrinks");
                }
                else if (user.Role == "Bartender")
                {
                    return RedirectToAction("Index", "OrderQueue");
                }
            }

            // If login fails, display an error message
            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View();
        }

        // GET: Account/Logout
        // Signs out the user and redirects to the login page
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Redirect to the login page
            return RedirectToAction("Index");
        }

        // GET: Account/Register
        // Displays the registration page
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        // Handles the registration form submission
        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            // Check if username already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser == null)
            {
                // Create a new user with the "Customer" role
                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    Role = "Customer" // Automatically assign "Customer" role
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Redirect to login page after successful registration
                return RedirectToAction("Index");
            }

            // If the username already exists, display an error message
            ViewBag.ErrorMessage = "Username already taken.";
            return View();
        }
    }
}
