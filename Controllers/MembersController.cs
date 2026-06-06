using DemoProjectWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemoProjectWebsite.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        // For demo purposes, using a static list. Replace with DB query.
        private static List<UserModel> _users = new List<UserModel>
    {
        new UserModel { Id = 1, FullName = "Andre Smith", Email="andre@example.com", Phone="1234567890", Gender="Male", Ethnicity="European", HairColor="Brown", YearOfBirth=2000 },
        new UserModel { Id = 2, FullName = "Jane Doe", Email="jane@example.com", Phone="0987654321", Gender="Female", Ethnicity="African", HairColor="Black", YearOfBirth=1995 }
    };

        [HttpGet]
        public IActionResult Index()
        {
            return View(_users);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null) return NotFound();
            return PartialView("_UserDetails", user); // returns popup content
        }
    }
}
