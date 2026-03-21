using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CinemaTicketSystem.Data;
using CinemaTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketSystem.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        var model = new EditProfileViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            ConcurrencyToken = user.ConcurrencyToken
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(EditProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user == null) return NotFound();
            _context.Entry(user).Property("ConcurrencyToken").OriginalValue = model.ConcurrencyToken;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.ConcurrencyToken = Guid.NewGuid();
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); 
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Old version, refresh website");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Database error occurred while updating user.");
            }
        }
        return View(model);
    }
}