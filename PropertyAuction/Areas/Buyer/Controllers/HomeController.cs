using Microsoft.AspNetCore.Mvc;
using Auction.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Auction.DataAccess.Data;

namespace PropertyAuction.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var activeAuctions = await _context.AuctionListings
                .Include(a => a.Property)
                .Where(a => a.EndDate > DateTime.Now && a.Status == AuctionStatus.Active)
                .ToListAsync();

            return View(activeAuctions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
