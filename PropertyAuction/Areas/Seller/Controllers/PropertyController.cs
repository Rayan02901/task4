using Microsoft.AspNetCore.Mvc;

namespace PropertyAuction.Areas.Seller.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
