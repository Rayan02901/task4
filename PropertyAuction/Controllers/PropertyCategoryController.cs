using Microsoft.AspNetCore.Mvc;
using Auction.DataAccess.Data;
using Auction.Models;

namespace PropertyAuction.Controllers
{
    public class PropertyCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PropertyCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<PropertyCategory> objCategoryList = _db.PropertyCategories.ToList();
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
                _db.PropertyCategories.Add(obj);
                _db.SaveChanges();
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
            PropertyCategory? propertycategoryFromDb = _db.PropertyCategories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (propertycategoryFromDb == null)
            {
                return NotFound();
            }
            return View(propertycategoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(PropertyCategory obj)
        {
            if (ModelState.IsValid)
            {
                _db.PropertyCategories.Update(obj);
                _db.SaveChanges();
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
            PropertyCategory? propertycategoryFromDb = _db.PropertyCategories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (propertycategoryFromDb == null)
            {
                return NotFound();
            }
            return View(propertycategoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            PropertyCategory? obj = _db.PropertyCategories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.PropertyCategories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
