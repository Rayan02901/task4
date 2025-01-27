using Auction.DataAccess.Repository.IRepository;
using Auction.Models;
using Auction.Utility;
using Microsoft.AspNetCore.Authorization;
using Auction.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace PropertyAuction.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = SD.Role_Seller + "," + SD.Role_Admin)]
    public class PropertyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PropertyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IActionResult Index()
        {
            // Get properties with their associated auction listings
            var properties = _unitOfWork.Property.GetAll(includeProperties: "PropertyCategory");
            var auctionListings = _unitOfWork.AuctionListing.GetAll();

            // Create a dictionary to quickly look up auction status by property ID
            var propertyAuctionStatus = auctionListings.ToDictionary(
                a => a.PropertyId,
                a => a.Status
            );

            // Add auction status information to ViewBag
            ViewBag.PropertyAuctionStatus = propertyAuctionStatus;

            string currentUserId = GetCurrentUserId();
            var isAdmin = User.IsInRole(SD.Role_Admin);

            // Filter properties based on user role
            IEnumerable<Property> objPropertyList;
            if (isAdmin)
            {
                objPropertyList = properties;
            }
            else
            {
                objPropertyList = properties.Where(p => p.SellerId == currentUserId);
            }

            return View(objPropertyList);
        }

        public IActionResult Create()
        {
            PropertyVM propertyVM = new()
            {
                PropertyCategories = _unitOfWork.PropertyCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Property = new Property()
            };
            return View(propertyVM);
        }

        [HttpPost]
        public IActionResult Create(PropertyVM propertyVM, IFormFile? imagefile, IFormFile? videofile)
        {
            if (ModelState.IsValid)
            {
                propertyVM.Property.SellerId = GetCurrentUserId();
                
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (imagefile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagefile.FileName);
                    string propertyPath = Path.Combine(wwwRootPath, @"images\property");
                    using (var fileStream = new FileStream(Path.Combine(propertyPath, fileName), FileMode.Create))
                    {
                        imagefile.CopyTo(fileStream);
                    }
                    propertyVM.Property.ImageUrl = @"\images\property\" + fileName;
                }

                if (videofile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(videofile.FileName);
                    string propertyPath = Path.Combine(wwwRootPath, @"videos\property");
                    using (var fileStream = new FileStream(Path.Combine(propertyPath, fileName), FileMode.Create))
                    {
                        videofile.CopyTo(fileStream);
                    }
                    propertyVM.Property.VideoUrl = @"\videos\property\" + fileName;
                }

                _unitOfWork.Property.Add(propertyVM.Property);
                _unitOfWork.Save();
                TempData["success"] = "Property created successfully";
                return RedirectToAction("Index");
            }

            propertyVM.PropertyCategories = _unitOfWork.PropertyCategory.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(propertyVM);
        }

        public IActionResult Edit(int id)
        {
            string currentUserId = GetCurrentUserId();
            var property = _unitOfWork.Property.Get(u => u.PropertyId == id);
            
            if (property == null || (!User.IsInRole(SD.Role_Admin) && property.SellerId != currentUserId))
            {
                return NotFound();
            }

            PropertyVM propertyVM = new()
            {
                PropertyCategories = _unitOfWork.PropertyCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Property = property
            };

            return View(propertyVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PropertyVM propertyVM, IFormFile? imagefile, IFormFile? videofile)
        {
            string currentUserId = GetCurrentUserId();
            if (!User.IsInRole(SD.Role_Admin) && propertyVM.Property.SellerId != currentUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (imagefile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagefile.FileName);
                    string propertyPath = Path.Combine(wwwRootPath, @"images\property");

                    if (!string.IsNullOrEmpty(propertyVM.Property.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, propertyVM.Property.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(propertyPath, fileName), FileMode.Create))
                    {
                        imagefile.CopyTo(fileStream);
                    }

                    propertyVM.Property.ImageUrl = @"\images\property\" + fileName;
                }

                if (videofile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(videofile.FileName);
                    string propertyPath = Path.Combine(wwwRootPath, @"videos\property");

                    if (!string.IsNullOrEmpty(propertyVM.Property.VideoUrl))
                    {
                        var oldVideoPath = Path.Combine(wwwRootPath, propertyVM.Property.VideoUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldVideoPath))
                        {
                            System.IO.File.Delete(oldVideoPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(propertyPath, fileName), FileMode.Create))
                    {
                        videofile.CopyTo(fileStream);
                    }

                    propertyVM.Property.VideoUrl = @"\videos\property\" + fileName;
                }

                _unitOfWork.Property.Update(propertyVM.Property);
                _unitOfWork.Save();
                TempData["success"] = "Property updated successfully";
                return RedirectToAction("Index");
            }

            propertyVM.PropertyCategories = _unitOfWork.PropertyCategory.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(propertyVM);
        }

        public IActionResult Delete(int? id)
        {
            string currentUserId = GetCurrentUserId();
            
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Property? propertyFromDb = _unitOfWork.Property.Get(u => u.PropertyId == id);
            if (propertyFromDb == null || (!User.IsInRole(SD.Role_Admin) && propertyFromDb.SellerId != currentUserId))
            {
                return NotFound();
            }

            PropertyVM propertyVM = new PropertyVM
            {
                Property = propertyFromDb,
                PropertyCategories = _unitOfWork.PropertyCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            return View(propertyVM);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            string currentUserId = GetCurrentUserId();
            
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Property? propertyToBeDeleted = _unitOfWork.Property.Get(u => u.PropertyId == id);
            if (propertyToBeDeleted == null || (!User.IsInRole(SD.Role_Admin) && propertyToBeDeleted.SellerId != currentUserId))
            {
                return NotFound();
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, propertyToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            var oldVideoPath = Path.Combine(_webHostEnvironment.WebRootPath, propertyToBeDeleted.VideoUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldVideoPath))
            {
                System.IO.File.Delete(oldVideoPath);
            }

            _unitOfWork.Property.Remove(propertyToBeDeleted);
            _unitOfWork.Save();

            TempData["success"] = "Property deleted successfully";
            return RedirectToAction("Index");
        }
    }
}