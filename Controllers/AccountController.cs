using Microsoft.AspNetCore.Mvc;
using CinemaTicketSystem.Data;
using CinemaTicketSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CinemaTicketSystem.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            if(_context.Users.Any(u=> u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "user with this email already exists");
                return View(model);
            }
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                PasswordHash = model.Password,
                Role = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        return View(model);
    } 
}