namespace CinemaTicketSystem.Services;

using CinemaTicketSystem.Data;
using CinemaTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string ErrorMessage)> EditUserProfileAsync(EditProfileViewModel model, int modifierUserId, bool isAdmin)
    {
        if (!isAdmin && model.Id != modifierUserId)
        {
            return (false, "Forbidden: You do not have permission to edit this profile.");
        }

        var user = new User { Id = model.Id, ConcurrencyToken = model.ConcurrencyToken };
        _context.Users.Attach(user);

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.PhoneNumber;
       // user.ConcurrencyToken = Guid.NewGuid();

        try
        {
            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }
        catch (DbUpdateConcurrencyException)
        {
            return (false, "Concurrency Conflict: Someone else edited this profile. Please refresh the page and try again.");
        }
        catch (DbUpdateException)
        {
            return (false, "Database error occurred while updating the profile.");
        }
    }

    public async Task<(bool Success, string ErrorMessage)> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return (false, "Not Found");
        
        _context.Users.Remove(user);
        
        try
        {
            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }
        catch (DbUpdateConcurrencyException)
        {
            return (false, "Concurrency Conflict: The user was already deleted or modified by another process.");
        }
        catch (DbUpdateException)
        {
            return (false, "Database error occurred while deleting the user.");
        }
    }
}
