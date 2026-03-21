using Microsoft.AspNetCore.Mvc;
using CinemaTicketSystem.Data;
using CinemaTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            if(await _context.Users.AnyAsync(u=> u.Email == model.Email))
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
                Role = "User",
                ConcurrencyToken = Guid.NewGuid()
            };
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("FullName", user.FirstName + " " + user.LastName)
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Database error occurred. Please try again later.");
                return View(model);
            }
        }
        return View(model);
    } 

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email == model.Email);
            if(user != null && user.PasswordHash == model.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("FullName", user.FirstName + " " + user.LastName)
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", principal);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Email or Password are incorrect");
        }
        return View(model);
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> EditProfile()
    {
        var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(claimId)) return Challenge();
        
        int currentUserId = int.Parse(claimId);
        var user = await _context.Users.FindAsync(currentUserId);
        
        if (user == null) return NotFound();

        var model = new EditProfileViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        };
        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(claimId)) return Challenge();
            
            int currentUserId = int.Parse(claimId);
            
            if (model.Id != currentUserId)
            {
                return Forbid();
            }

            var user = await _context.Users.FindAsync(model.Id);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            try 
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Database error occurred while updating profile.");
            }
        }
        return View(model);
    }
}