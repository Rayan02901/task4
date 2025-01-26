﻿using Auction.DataAccess.Data;
using Auction.Models;
using Auction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PropertyAuction.Areas.Buyer.Controllers
{
    
    public class BuyerAuctionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuyerAuctionController(ApplicationDbContext context)
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

        public async Task<IActionResult> Details(int id)
        {
            var auction = await _context.AuctionListings
                .Include(a => a.Property)
                .FirstOrDefaultAsync(a => a.AuctionId == id);

            if (auction == null)
            {
                return NotFound();
            }

            var viewModel = new AuctionDetailsForBuyerVM
            {
                AuctionId = auction.AuctionId,
                Property = auction.Property,
                StartDate = auction.StartDate,
                EndDate = auction.EndDate,
                StartingBid = auction.StartingBid,
                CurrentHighestBid = auction.CurrentHighestBid ?? auction.StartingBid,
                ReservationPrice = auction.ReservationPrice,
                MinimumBidIncrement = auction.MinimumBidIncrement,
                Status = auction.Status,

                // Ensure RecentBids is assigned an empty List<Bid> if auction.Bids is null
                RecentBids = auction.Bids?.ToList() ?? new List<Bid>()
            };
            return View(viewModel);


            
        }
        [HttpPost]

        public async Task<IActionResult> PlaceBid(int auctionId, decimal bidAmount)
        {
            var auction = await _context.AuctionListings
                .Include(a => a.Bids) // Ensure we load the Bids collection
                .FirstOrDefaultAsync(a => a.AuctionId == auctionId);

            if (auction == null || auction.Status != AuctionStatus.Active)
            {
                return BadRequest("Auction is not active.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (bidAmount < auction.StartingBid || (auction.CurrentHighestBid.HasValue && bidAmount <= auction.CurrentHighestBid))
            {
                return BadRequest("Bid amount must be higher than the starting bid or the current highest bid.");
            }

            // Create the new bid
            var bid = new Bid
            {
                AuctionId = auctionId,
                UserId = userId,
                BidAmount = bidAmount,
                BidTime = DateTime.Now
            };

            // Add the bid to the Bids collection
            _context.Bids.Add(bid);

            // Update the auction's current highest bid and highest bidder
            auction.CurrentHighestBid = bidAmount;
            auction.HighestBidderId = userId;

            // Check if the reservation price is met
            auction.IsReservationPriceMet = auction.CurrentHighestBid >= auction.ReservationPrice;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = auctionId });
        }

    }
}
