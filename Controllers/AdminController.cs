using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CinemaTicketSystem.Data;
using CinemaTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaTicketSystem.Services;
using System.Security.Claims;

namespace CinemaTicketSystem.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public AdminController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.ToListAsync();
        var screenings = await _context.Screenings.Include(s => s.Cinema).ToListAsync();
        
        var model = new AdminDashboardViewModel
        {
            Users = users,
            Screenings = screenings
        };

        return View(model);
    }
    [HttpGet]
    public IActionResult CreateScreening()
    {
        ViewBag.Cinemas = new SelectList(_context.Cinemas,"Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateScreening(Screening model)
    {
        if(ModelState.IsValid)
        {
            _context.Screenings.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        ViewBag.Cinemas = new SelectList(_context.Cinemas, "Id", "Name");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteScreening(int id)
    {
        var screening = await _context.Screenings.FindAsync(id);
        if(screening != null)
        {
            _context.Screenings.Remove(screening);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        var reservations = await _context.Reservations
            .Include(r => r.Screening)
            .ThenInclude(s => s.Cinema)
            .Where(r => r.UserId == id)
            .OrderBy(r => r.Screening.StartTime)
            .ToListAsync();

        var reservationItems = new List<AdminReservationItemViewModel>();
        
        foreach (var r in reservations)
        {
            var screeningReservations = await _context.Reservations
                .Where(or => or.ScreeningId == r.ScreeningId)
                .ToListAsync();

            var freeSeats = new List<SelectListItem>();
            
            for (int row = 1; row <= (r.Screening?.Cinema?.Rows ?? 0); row++)
            {
                for (int seat = 1; seat <= (r.Screening?.Cinema?.SeatsPerRow ?? 0); seat++)
                {
                    bool isTaken = screeningReservations.Any(sr => sr.RowPosition == row && sr.SeatPosition == seat);
                    
                    if (!isTaken || (r.RowPosition == row && r.SeatPosition == seat))
                    {
                        freeSeats.Add(new SelectListItem 
                        { 
                            Value = $"{row}_{seat}", 
                            Text = $"Row {row}, Seat {seat}" 
                        });
                    }
                }
            }

            reservationItems.Add(new AdminReservationItemViewModel
            {
                ReservationId = r.Id,
                ScreeningId = r.ScreeningId,
                MovieTitle = r.Screening?.MovieTitle ?? "Unknown",
                CinemaName = r.Screening?.Cinema?.Name ?? "Unknown Room",
                StartTime = r.Screening?.StartTime ?? DateTime.MinValue,
                CurrentRow = r.RowPosition,
                CurrentSeat = r.SeatPosition,
                FreeSeatsOptions = freeSeats,
                SelectedNewSeat = $"{r.RowPosition}_{r.SeatPosition}"
            });
        }

        var model = new AdminEditUserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            ConcurrencyToken = user.ConcurrencyToken,
            Reservations = reservationItems
        };
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(AdminEditUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(claimId)) return Challenge();
            
            int currentAdminId = int.Parse(claimId);
            
            var profileModel = new EditProfileViewModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ConcurrencyToken = model.ConcurrencyToken
            };

            var result = await _userService.EditUserProfileAsync(profileModel, currentAdminId, isAdmin: true);
            
            if (result.Success)
            {
                return RedirectToAction(nameof(Index)); 
            }
            TempData["ErrorMessage"] = result.ErrorMessage;
        }
        return RedirectToAction(nameof(EditUser), new { id = model.Id });
    }

    [HttpPost]
    public async Task<IActionResult> ChangeUserSeat(int userId, int reservationId, string selectedNewSeat)
    {
        if (string.IsNullOrEmpty(selectedNewSeat)) 
            return RedirectToAction(nameof(EditUser), new { id = userId });

        var parts = selectedNewSeat.Split('_');
        if (parts.Length != 2 || !int.TryParse(parts[0], out int newRow) || !int.TryParse(parts[1], out int newSeat))
            return RedirectToAction(nameof(EditUser), new { id = userId });

        var reservation = await _context.Reservations.FindAsync(reservationId);
        if (reservation == null) return NotFound();
        reservation.RowPosition = newRow;
        reservation.SeatPosition = newSeat;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Could not move seat. It may have been taken by another user.";
        }

        return RedirectToAction(nameof(EditUser), new { id = userId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUserReservation(int userId, int reservationId)
    {
        var reservation = await _context.Reservations.FindAsync(reservationId);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(EditUser), new { id = userId });
    }
}