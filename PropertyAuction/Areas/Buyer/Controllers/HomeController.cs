using Microsoft.AspNetCore.Mvc;
using Auction.Models;
using Auction.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Diagnostics;
using Auction.DataAccess.Repository.IRepository;

namespace PropertyAuction.Controllers
{
    [Area("Buyer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var activeAuctions = _unitOfWork.AuctionListing
                .GetAll(includeProperties: "Property")
                .Where(a => a.EndDate > DateTime.Now && a.Status == AuctionStatus.Active)
                .ToList();
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