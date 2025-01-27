using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using Auction.Models.ViewModels;
using Auction.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PropertyAuction.Areas.Buyer.Controllers
{
    [Area("Buyer")]
    [Authorize(Roles = SD.Role_Buyer)]

    public class BuyerAuctionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuyerAuctionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            // Get the auctions
            var auctions = await Task.FromResult(_unitOfWork.AuctionListing
                .GetAll(includeProperties: "Property")
                .Select(a => new BuyerAuctionListVM  
                {
                    AuctionId = a.AuctionId,
                    PropertyTitle = a.Property.Title,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    StartingBid = a.StartingBid,
                    CurrentHighestBid = a.CurrentHighestBid ?? a.StartingBid,
                    Status = a.Status
                })
                .ToList());

            return View(auctions);  
        }


        public async Task<IActionResult> Details(int id)
        {
            var auction = await Task.FromResult(_unitOfWork.AuctionListing
                .Get(a => a.AuctionId == id, includeProperties: "Property,Bids"));

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
                RecentBids = auction.Bids?.ToList() ?? new List<Bid>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PlaceBid(int auctionId, decimal bidAmount)
        {
            var auction = _unitOfWork.AuctionListing
                .Get(a => a.AuctionId == auctionId, includeProperties: "Bids");

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
            _unitOfWork.Bids.Add(bid);

            // Update the auction's current highest bid and highest bidder
            auction.CurrentHighestBid = bidAmount;
            auction.HighestBidderId = userId;

            // Check if the reservation price is met
            auction.IsReservationPriceMet = auction.CurrentHighestBid >= auction.ReservationPrice;

            // Update the auction listing
            _unitOfWork.AuctionListing.Update(auction);

            // Save changes
            _unitOfWork.Save();

            return RedirectToAction(nameof(Details), new { id = auctionId });
        }
        public async Task<IActionResult> BidHistory()
        {
            // Get the currently logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            // Retrieve bids made by this user
            var userBids = await Task.FromResult(_unitOfWork.Bids
                .GetAll(
                    b => b.UserId == userId,
                    includeProperties: "Auction.Property" // Ensure includeProperties is specified only once
                )
                .Select(b => new BidHistoryVM
                {
                    BidId = b.Id,
                    AuctionId = b.AuctionId,
                    PropertyTitle = b.Auction.Property.Title,
                    BidAmount = b.BidAmount,
                    BidTime = b.BidTime,
                    AuctionStatus = b.Auction.Status,
                    AuctionEndDate = b.Auction.EndDate
                })
                .OrderByDescending(b => b.BidTime)
                .ToList());

            return View(userBids);
        }

    }
}