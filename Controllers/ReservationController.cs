using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CinemaTicketSystem.Data;
using CinemaTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CinemaTicketSystem.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SeatMap(int screeningId)
    {
        var screening = await _context.Screenings
            .Include(s => s.Cinema)
            .FirstOrDefaultAsync(s => s.Id == screeningId);

        if (screening == null)
            return NotFound("Screening not found.");

        var reservations = await _context.Reservations
            .Where(r => r.ScreeningId == screeningId)
            .ToListAsync();

        int currentUserId = 0;
        if (User.Identity?.IsAuthenticated == true)
        {
            var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(claimId))
            {
                currentUserId = int.Parse(claimId);
            }
        }

        var model = new SeatMapViewModel
        {
            Screening = screening,
            ExistingReservations = reservations,
            CurrentUserId = currentUserId
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookSeat(int screeningId, string seatCoordinates)
    {
        if (string.IsNullOrEmpty(seatCoordinates))
        {
            TempData["ErrorMessage"] = "Please select a seat.";
            return RedirectToAction(nameof(SeatMap), new { screeningId = screeningId });
        }

        var parts = seatCoordinates.Split('-');
        if (parts.Length != 2 || !int.TryParse(parts[0], out int rowPosition) || !int.TryParse(parts[1], out int seatPosition))
        {
            TempData["ErrorMessage"] = "Invalid seat selection.";
            return RedirectToAction(nameof(SeatMap), new { screeningId = screeningId });
        }

        var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(claimId)) return Challenge();
        
        int currentUserId = int.Parse(claimId);

        var reservation = new Reservation
        {
            ScreeningId = screeningId,
            UserId = currentUserId,
            RowPosition = rowPosition,
            SeatPosition = seatPosition
        };

        _context.Reservations.Add(reservation);

        try
        {
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SeatMap), new { screeningId = screeningId });
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Seat is already taken or another database error occurred.";
            return RedirectToAction(nameof(SeatMap), new { screeningId = screeningId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CancelSeat(int reservationId, int screeningId)
    {
        var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(claimId)) return Challenge();
        
        int currentUserId = int.Parse(claimId);

        var reservation = await _context.Reservations.FindAsync(reservationId);
        
        if (reservation == null)
            return RedirectToAction(nameof(SeatMap), new { screeningId = screeningId });

        if (reservation.UserId != currentUserId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        if (screeningId == 0)
        {
            return RedirectToAction("EditProfile", "Account");
        }

        return RedirectToAction(nameof(SeatMap), new { screeningId = screeningId });
    }
}
