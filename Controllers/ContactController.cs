using DemoProjectWebsite.Data;
using DemoProjectWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectWebsite.Controllers
{
    [Authorize]
    public class ContactController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // initialize to non-null defaults to avoid null-assign warnings
            var model = new ContactViewModel
            {
                Name = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Address = string.Empty,
                Message = string.Empty
            };

            if (User?.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    // Pre-populate Email from Identity (guard nulls)
                    model.Name = user.FullName ?? model.Name;
                    model.Email = user.Email ?? model.Email;

                    // Only query if we have a non-empty email
                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        var existingContact = _context.Contacts.FirstOrDefault(c => c.Email == user.Email);
                        if (existingContact != null)
                        {
                            model.Name = existingContact.Name ?? user.FullName ?? model.Name;
                            // coalesce nullable fields back to model to ensure non-null assignment
                            model.Phone = existingContact.Phone ?? model.Phone;
                            model.Address = existingContact.Address ?? model.Address;
                            model.Message = existingContact.Message ?? model.Message;
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get current Identity user (may be null)
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
                        // Use user?.FullName to avoid dereference if user is null
                        Name = model.Name ?? user?.FullName ?? string.Empty,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address,
                        Message = model.Message
                    };
                    _context.Contacts.Add(contact);
                }
                else
                {
                    contact.Name = model.Name ?? user?.FullName ?? contact.Name;
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

