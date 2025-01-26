using Auction.DataAccess.Repository.IRepository;
using Auction.DataAccess.Data;
using Auction.Models;
using Auction.Utility;
using Microsoft.AspNetCore.Authorization;
using Auction.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace PropertyAuction.Controllers
{
    
    public class PropertyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PropertyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Property> objCategoryList = _unitOfWork.Property.GetAll().ToList();
            return View(objCategoryList);
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
            else
            {
                propertyVM.PropertyCategories = _unitOfWork.PropertyCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(propertyVM);
            }

        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            PropertyVM propertyVM = new()
            {
                PropertyCategories = _unitOfWork.PropertyCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Property = _unitOfWork.Property.Get(u => u.PropertyId == id)
            };

            if (propertyVM.Property == null)
            {
                return NotFound();
            }

            return View(propertyVM);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PropertyVM propertyVM, IFormFile? imagefile, IFormFile? videofile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // File upload logic
                if (imagefile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagefile.FileName);
                    string propertyPath = Path.Combine(wwwRootPath, @"images\property");

                    // Delete old image if exists
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

                    // Delete old image if exists
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

                    propertyVM.Property.ImageUrl = @"\videos\property\" + fileName;
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
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Retrieve the property and populate PropertyVM
            Property? propertyFromDb = _unitOfWork.Property.Get(u => u.PropertyId == id);
            if (propertyFromDb == null)
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
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Retrieve the property to delete
            Property? propertyToBeDeleted = _unitOfWork.Property.Get(u => u.PropertyId == id);
            if (propertyToBeDeleted == null)
            {
                return NotFound();
            }

            // Handle image deletion
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, propertyToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            // Handle video deletion (if applicable)
            var oldVideoPath = Path.Combine(_webHostEnvironment.WebRootPath, propertyToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldVideoPath))
            {
                System.IO.File.Delete(oldVideoPath);
            }

            // Remove the property
            _unitOfWork.Property.Remove(propertyToBeDeleted);
            _unitOfWork.Save();

            TempData["success"] = "Property deleted successfully";
            return RedirectToAction("Index");
        }


    }
}