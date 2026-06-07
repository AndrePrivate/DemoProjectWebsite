using DemoProjectWebsite.Data;
using DemoProjectWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectWebsite.Controllers
{
    [Authorize]
    public class MoreInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoreInfoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var existingInfo = _context.MoreInfos.FirstOrDefault(m => m.UserId == user.Id);

            var model = new MoreInfoViewModel();
            if (existingInfo != null)
            {
                model.Gender = existingInfo.Gender;
                model.Ethnicity = existingInfo.Ethnicity;
                model.HairColor = existingInfo.HairColor;
                model.YearOfBirth = existingInfo.YearOfBirth;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MoreInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var existingInfo = _context.MoreInfos.FirstOrDefault(m => m.UserId == user.Id);

                if (existingInfo == null)
                {
                    var info = new MoreInfoModel
                    {
                        UserId = user.Id,
                        Gender = model.Gender,
                        Ethnicity = model.Ethnicity,
                        HairColor = model.HairColor,
                        YearOfBirth = model.YearOfBirth
                    };
                    _context.MoreInfos.Add(info);
                }
                else
                {
                    existingInfo.Gender = model.Gender;
                    existingInfo.Ethnicity = model.Ethnicity;
                    existingInfo.HairColor = model.HairColor;
                    existingInfo.YearOfBirth = model.YearOfBirth;
                    _context.MoreInfos.Update(existingInfo);
                }

                _context.SaveChanges();
                ViewBag.Message = "Information submitted successfully.";
            }

            return View(model);
        }
    }
}
