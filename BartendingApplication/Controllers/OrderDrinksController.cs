using BartendingApplication.Models;
using Microsoft.AspNetCore.Mvc;

public class OrderDrinksController : Controller
{
    private readonly BartenderDbContext _context;

    // Constructor to initialize the BartenderDbContext
    public OrderDrinksController(BartenderDbContext context)
    {
        _context = context;
    }

    // GET: OrderDrinks
    // Displays the list of drinks with pagination
    public IActionResult Index(int page = 1)
    {
        int pageSize = 5;

        // Fetch a page of drinks from the database
        var drinks = _context.CocktailMenus
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Calculate the total number of pages
        var totalDrinks = _context.CocktailMenus.Count();
        var totalPages = (int)Math.Ceiling(totalDrinks / (double)pageSize);

        // Pass data to the view
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewData["SuccessMessage"] = TempData["SuccessMessage"];

        return View(drinks);
    }

    // POST: OrderDrink
    // Places an order for a selected drink
    [HttpPost]
    public IActionResult Order(int id)
    {
        var drink = _context.CocktailMenus.Find(id);
        var userName = User.Identity?.Name;

        if (drink != null && !string.IsNullOrEmpty(userName))
        {
            // Create a new order for the selected drink
            var order = new CocktailOrder
            {
                CocktailName = drink.Name, // Using drink name directly
                UserName = userName, // Using username
                Status = "Order Placed"
            };

            _context.CocktailOrders.Add(order);
            _context.SaveChanges();

            // Set the success message in TempData
            TempData["SuccessMessage"] = "Order Placed! Your server will be with you shortly!";

            // Reload the page with the success message
            return RedirectToAction("Index");
        }

        // If the drink is not found or username is invalid, reload the page with an error message
        TempData["ErrorMessage"] = "Unable to place order. Please try again.";
        return RedirectToAction("Index");
    }
}
