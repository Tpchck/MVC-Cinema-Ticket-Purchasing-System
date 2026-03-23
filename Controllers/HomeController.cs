using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CinemaTicketSystem.Models;
using CinemaTicketSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketSystem.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var screenings = await _context.Screenings.Include(s => s.Cinema).OrderBy(s => s.StartTime).ToListAsync();
        return View(screenings);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
