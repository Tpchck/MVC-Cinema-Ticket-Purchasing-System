using System.ComponentModel.DataAnnotations;

namespace CinemaTicketSystem.Models;

public class EditProfileViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "first name required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "last name required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "phone number required")]
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid ConcurrencyToken { get; set; }

    public List<UserReservationItemViewModel> Reservations { get; set; } = new();
}

public class UserReservationItemViewModel
{
    public int ReservationId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public string CinemaName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int RowPosition { get; set; }
    public int SeatPosition { get; set; }
}