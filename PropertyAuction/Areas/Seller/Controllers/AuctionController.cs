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
        public async Task<IActionResult> Index()
        {
            var auctions = await _context.AuctionListings
                .Include(a => a.Property)
                .Select(a => new AuctionListVM
                {
                    AuctionId = a.AuctionId,
                    PropertyTitle = a.Property.Title,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    StartingBid = a.StartingBid,
                    CurrentHighestBid = a.CurrentHighestBid ?? a.StartingBid,
                    Status = a.Status
                })
                .ToListAsync();

            return View(auctions);
        }

        public async Task<IActionResult> Create(int id)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (property == null)
            {
                return NotFound();
            }

            var viewModel = new AuctionCreateVM
            {
                PropertyId = property.PropertyId,
                SelectedProperty = property,
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
                    Status = AuctionStatus.Active,
                    CurrentHighestBid = model.StartingBid,
                    IsReservationPriceMet = false,
                    IsBidStarted = false
                };

                _context.AuctionListings.Add(auctionListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload property if model is invalid
            model.SelectedProperty = await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyId == model.PropertyId);

            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var auction = await _context.AuctionListings
                .Include(a => a.Property)
                .FirstOrDefaultAsync(a => a.AuctionId == id);

            if (auction == null)
            {
                return NotFound();
            }

            var viewModel = new SellerAuctionDetailsVM
            {
                AuctionId = auction.AuctionId,
                Property = auction.Property,
                StartDate = auction.StartDate,
                EndDate = auction.EndDate,
                StartingBid = auction.StartingBid,
                CurrentHighestBid = auction.CurrentHighestBid ?? auction.StartingBid,
                ReservationPrice = auction.ReservationPrice,
                MinimumBidIncrement = auction.MinimumBidIncrement,
                IsReservationPriceMet = auction.IsReservationPriceMet,
                IsBidStarted = auction.IsBidStarted,
                Status = auction.Status,
                HighestBidderId = auction.HighestBidderId
            };

            return View(viewModel);
        }
    }
}
