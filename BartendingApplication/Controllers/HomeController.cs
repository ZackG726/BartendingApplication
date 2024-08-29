using Microsoft.AspNetCore.Mvc;
using BartendingApplication.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BartendingApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly BartenderDbContext _context;

        // Constructor to initialize the BartenderDbContext
        public HomeController(BartenderDbContext context)
        {
            _context = context;
        }

        // GET: /Home/OrderDrinks
        // Displays the order drinks page
        public IActionResult OrderDrinks()
        {
            return View();
        }

        // GET: /Home/OrderQueue
        // Displays the order queue page
        public IActionResult OrderQueue()
        {
            return View();
        }
    }
}
