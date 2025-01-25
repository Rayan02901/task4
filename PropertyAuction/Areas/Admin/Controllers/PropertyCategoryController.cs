using Auction.DataAccess.Repository.IRepository;
using Auction.DataAccess.Data;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;

namespace PropertyAuction.Controllers
{

    public class PropertyCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PropertyCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<PropertyCategory> objCategoryList = _unitOfWork.PropertyCategory.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PropertyCategory obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.PropertyCategory.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "PropertyCategory created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            PropertyCategory? categoryFromDb = _unitOfWork.PropertyCategory.Get(u => u.Id == id);
            //PropertyCategory? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //PropertyCategory? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(PropertyCategory obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PropertyCategory.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "PropertyCategory updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            PropertyCategory? categoryFromDb = _unitOfWork.PropertyCategory.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            PropertyCategory? obj = _unitOfWork.PropertyCategory.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.PropertyCategory.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "PropertyCategory deleted successfully";
            return RedirectToAction("Index");
        }
    }
}