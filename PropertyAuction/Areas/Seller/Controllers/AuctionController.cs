using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using Auction.Utility;
using Microsoft.AspNetCore.Authorization;
using Auction.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace PropertyAuction.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = SD.Role_Seller + "," + SD.Role_Admin)]
    public class AuctionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuctionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IActionResult Index()
        {
            string currentUserId = GetCurrentUserId();
            var isAdmin = User.IsInRole(SD.Role_Admin);

            var query = _unitOfWork.AuctionListing
                .GetAll(includeProperties: "Property");

            if (!isAdmin)
            {
                query = query.Where(a => a.Property.SellerId == currentUserId);
            }

            var auctions = query.Select(a => new AuctionListVM
            {
                AuctionId = a.AuctionId,
                PropertyTitle = a.Property.Title,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                StartingBid = a.StartingBid,
                CurrentHighestBid = a.CurrentHighestBid ?? a.StartingBid,
                Status = a.Status
            })
            .ToList();

            return View(auctions);
        }

        public IActionResult Create(int id)
        {
            string currentUserId = GetCurrentUserId();
            var property = _unitOfWork.Property.Get(p => p.PropertyId == id);

            if (property == null || (!User.IsInRole(SD.Role_Admin) && property.SellerId != currentUserId))
            {
                return NotFound();
            }

            // Check if property already has an active auction
            var existingAuction = _unitOfWork.AuctionListing
                .Get(a => a.PropertyId == id && a.Status == AuctionStatus.Active);

            if (existingAuction != null)
            {
                TempData["error"] = "This property already has an active auction.";
                return RedirectToAction(nameof(Index));
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
        public IActionResult Create(AuctionCreateVM model)
        {
            string currentUserId = GetCurrentUserId();
            var property = _unitOfWork.Property.Get(p => p.PropertyId == model.PropertyId);

            if (property == null || (!User.IsInRole(SD.Role_Admin) && property.SellerId != currentUserId))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Additional validation
                if (model.StartDate < DateTime.Now)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be in the past");
                    model.SelectedProperty = property;
                    return View(model);
                }

                if (model.EndDate <= model.StartDate)
                {
                    ModelState.AddModelError("EndDate", "End date must be after start date");
                    model.SelectedProperty = property;
                    return View(model);
                }

                if (model.StartingBid <= 0)
                {
                    ModelState.AddModelError("StartingBid", "Starting bid must be greater than zero");
                    model.SelectedProperty = property;
                    return View(model);
                }

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

                _unitOfWork.AuctionListing.Add(auctionListing);
                _unitOfWork.Save();
                TempData["success"] = "Auction created successfully";
                return RedirectToAction(nameof(Index));
            }

            model.SelectedProperty = property;
            return View(model);
        }

        public IActionResult Details(int id)
        {
            string currentUserId = GetCurrentUserId();
            var auction = _unitOfWork.AuctionListing
                .Get(a => a.AuctionId == id, includeProperties: "Property,Bids");

            if (auction == null || (!User.IsInRole(SD.Role_Admin) && auction.Property.SellerId != currentUserId))
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
                HighestBidderId = auction.HighestBidderId,
                Bids = auction.Bids?.OrderByDescending(b => b.BidAmount).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Report()
        {
            string currentUserId = GetCurrentUserId();
            var isAdmin = User.IsInRole(SD.Role_Admin);

            var query = _unitOfWork.AuctionListing
                .GetAll(
                    filter: a => a.Status == AuctionStatus.Completed,
                    includeProperties: "Bids,Property"
                );

            if (!isAdmin)
            {
                query = query.Where(a => a.Property.SellerId == currentUserId);
            }

            var completedAuctions = await Task.FromResult(query
                .Select(a => new AuctionReportVM
                {
                    AuctionId = a.AuctionId,
                    PropertyTitle = a.Property.Title,
                    WinningBid = a.CurrentHighestBid ?? 0,
                    
                    HighestBidderName = a.HighestBidderId != null
                        ? _unitOfWork.ApplicationUser.Get(u => u.Id == a.HighestBidderId)?.Name
                        : "No Bids",
                    
                })
                .ToList());

            return View(completedAuctions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            string currentUserId = GetCurrentUserId();
            var auction = _unitOfWork.AuctionListing
                .Get(a => a.AuctionId == id, includeProperties: "Property");

            if (auction == null || (!User.IsInRole(SD.Role_Admin) && auction.Property.SellerId != currentUserId))
            {
                return NotFound();
            }

            if (auction.Status != AuctionStatus.Active || auction.IsBidStarted)
            {
                TempData["error"] = "Cannot cancel auction that has already received bids or is not active.";
                return RedirectToAction(nameof(Details), new { id = id });
            }

            auction.Status = AuctionStatus.Completed;
            _unitOfWork.AuctionListing.Update(auction);
            _unitOfWork.Save();

            TempData["success"] = "Auction cancelled successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}