namespace CinemaTicketSystem.Services;

using CinemaTicketSystem.Models;

public interface IUserService
{
    Task<(bool Success, string ErrorMessage)> EditUserProfileAsync(EditProfileViewModel model, int modifierUserId, bool isAdmin);
    Task<(bool Success, string ErrorMessage)> DeleteUserAsync(int userId);
}
