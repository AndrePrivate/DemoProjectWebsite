using DemoProjectWebsite.Data;
using DemoProjectWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemoProjectWebsite.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var members = (from user in _context.Users
                           join contact in _context.Contacts on user.Email equals contact.Email into contactJoin
                           from contact in contactJoin.DefaultIfEmpty()
                           join info in _context.MoreInfos on user.Id equals info.UserId into infoJoin
                           from info in infoJoin.DefaultIfEmpty()
                           select new UserModel
                           {
                               Id = user.Id, // string GUID
                               FullName = contact != null ? contact.Name : (user.FullName ?? user.Email ?? string.Empty),
                               Email = user.Email ?? string.Empty,
                               Phone = contact != null ? contact.Phone ?? string.Empty : string.Empty,
                               Gender = info != null ? info.Gender ?? string.Empty : string.Empty,
                               Ethnicity = info != null ? info.Ethnicity ?? string.Empty : string.Empty,
                               HairColor = info != null ? info.HairColor ?? string.Empty : string.Empty,
                               YearOfBirth = info != null ? info.YearOfBirth : 0
                           }).AsEnumerable().ToList();

            return View(members);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var member = (from user in _context.Users
                          where user.Id == id
                          join contact in _context.Contacts on user.Email equals contact.Email into contactJoin
                          from contact in contactJoin.DefaultIfEmpty()
                          join info in _context.MoreInfos on user.Id equals info.UserId into infoJoin
                          from info in infoJoin.DefaultIfEmpty()
                          select new UserModel
                          {
                              Id = user.Id,
                              FullName = contact != null ? contact.Name : (user.FullName ?? user.Email ?? string.Empty),
                              Email = user.Email ?? string.Empty,
                              Phone = contact != null ? contact.Phone ?? string.Empty : string.Empty,
                              Gender = info != null ? info.Gender ?? string.Empty : string.Empty,
                              Ethnicity = info != null ? info.Ethnicity ?? string.Empty : string.Empty,
                              HairColor = info != null ? info.HairColor ?? string.Empty : string.Empty,
                              YearOfBirth = info != null ? info.YearOfBirth : 0
                          }).AsEnumerable().FirstOrDefault();

            if (member == null) return NotFound();
            return PartialView("_UserDetails", member);
        }
    }
}
