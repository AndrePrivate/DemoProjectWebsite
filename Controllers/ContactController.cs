using DemoProjectWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectWebsite.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save to database or send email
                ViewBag.Message = "Your contact details have been submitted successfully.";
                return View();
            }
            return View(model);
        }
    }
}

