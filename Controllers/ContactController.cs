using DemoProjectWebsite.Data;
using DemoProjectWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectWebsite.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ContactViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                // Pre‑populate Email from Identity
                model.Email = user.Email;

                // Check if a Contact record already exists for this email
                var existingContact = _context.Contacts.FirstOrDefault(c => c.Email == user.Email);
                if (existingContact != null)
                {
                    model.Name = existingContact.Name;
                    model.Phone = existingContact.Phone;
                    model.Address = existingContact.Address;
                    model.Message = existingContact.Message;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get current Identity user
                var user = await _userManager.GetUserAsync(User);

                if (user != null && user.Email != model.Email)
                {
                    // Update Identity user email + username
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                    {
                        foreach (var error in updateResult.Errors)
                            ModelState.AddModelError("", error.Description);

                        return View(model);
                    }
                }

                // Update or insert Contact record
                var contact = _context.Contacts.FirstOrDefault(c => c.Email == model.Email);
                if (contact == null)
                {
                    contact = new ContactModel
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address,
                        Message = model.Message
                    };
                    _context.Contacts.Add(contact);
                }
                else
                {
                    contact.Name = model.Name;
                    contact.Phone = model.Phone;
                    contact.Address = model.Address;
                    contact.Message = model.Message;
                    _context.Contacts.Update(contact);
                }

                _context.SaveChanges();
                ViewBag.Message = "Your contact details (and login email if changed) have been saved successfully.";
            }

            return View(model);
        }
    }
}

