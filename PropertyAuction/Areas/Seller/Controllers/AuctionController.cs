using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using Auction.Utility;
using Microsoft.AspNetCore.Authorization;
using Auction.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public IActionResult Index()
        {
            var auctions = _unitOfWork.AuctionListing
                .GetAll(includeProperties: "Property")
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
                .ToList();

            return View(auctions);
        }

        public IActionResult Create(int id)
        {
            var property = _unitOfWork.Property
                .Get(p => p.PropertyId == id);

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
        public IActionResult Create(AuctionCreateVM model)
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

                _unitOfWork.AuctionListing.Add(auctionListing);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            // Reload property if model is invalid
            model.SelectedProperty = _unitOfWork.Property
                .Get(p => p.PropertyId == model.PropertyId);

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var auction = _unitOfWork.AuctionListing
                .Get(a => a.AuctionId == id, includeProperties: "Property");

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