using Auction.DataAccess.Data;
using Auction.Models;
using Auction.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace PropertyAuction.Areas.Seller.Controllers
{
    public class AuctionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuctionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new AuctionCreateVM
            {
                AvailableProperties = await _context.Properties
                    .Where(p => !_context.AuctionListings.Any(a => a.PropertyId == p.PropertyId))
                    .ToListAsync(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuctionCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var auctionListing = new AuctionListing
                {
                    PropertyId = model.PropertyId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    ReservationPrice = model.ReservationPrice,
                    MinimumBidIncrement = model.MinimumBidIncrement,
                    StartingBid = model.StartingBid,
                    
                    CurrentHighestBid = model.StartingBid
                };

                _context.AuctionListings.Add(auctionListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate properties if model is invalid
            model.AvailableProperties = await _context.Properties
                .Where(p => !_context.AuctionListings.Any(a => a.PropertyId == p.PropertyId))
                .ToListAsync();

            return View(model);
        }
    }
}
