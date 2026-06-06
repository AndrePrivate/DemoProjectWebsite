using DemoProjectWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectWebsite.Controllers
{
    [Authorize]
    public class MoreInfoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MoreInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Information submitted successfully.";
                return View();
            }
            return View(model);
        }
    }
}
