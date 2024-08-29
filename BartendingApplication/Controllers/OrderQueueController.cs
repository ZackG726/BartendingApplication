using BartendingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BartendingApplication.Controllers
{
    public class OrderQueueController : Controller
    {
        private readonly BartenderDbContext _context;

        // Constructor to initialize the BartenderDbContext
        public OrderQueueController(BartenderDbContext context)
        {
            _context = context;
        }

        // GET: OrderQueue
        // Displays the list of cocktail orders
        public IActionResult Index()
        {
            // Fetch all orders from the database
            var orders = _context.CocktailOrders.ToList();
            return View(orders);
        }

        // POST: AdvanceStatus
        // Advances the status of an order
        [HttpPost]
        public IActionResult AdvanceStatus(int id, string currentStatus)
        {
            // Find the order by ID
            var order = _context.CocktailOrders.Find(id);

            if (order != null)
            {
                // Update status based on the current status
                if (currentStatus == "Order Placed")
                {
                    order.Status = "Order Ready For Pickup";
                }
                else if (currentStatus == "Order Ready For Pickup")
                {
                    order.Status = "Order Completed";
                }

                // Save changes to the database
                _context.SaveChanges();
            }

            // Redirect back to the Index page
            return RedirectToAction("Index");
        }

        // POST: Delete
        // Deletes an order
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Find the order by ID
            var order = _context.CocktailOrders.Find(id);
            if (order != null)
            {
                // Remove the order from the database
                _context.CocktailOrders.Remove(order);
                _context.SaveChanges();
            }
            // Redirect back to the Index page
            return RedirectToAction("Index");
        }
    }
}
