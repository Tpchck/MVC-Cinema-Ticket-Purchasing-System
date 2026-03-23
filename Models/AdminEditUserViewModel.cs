namespace CinemaTicketSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AdminEditUserViewModel : EditProfileViewModel
{
    public new List<AdminReservationItemViewModel> Reservations { get; set; } = new();
}

public class AdminReservationItemViewModel
{
    public int ReservationId { get; set; }
    public int ScreeningId { get; set; }
    public string MovieTitle { get; set; } = "";
    public string CinemaName { get; set; } = "";
    public DateTime StartTime { get; set; }
    public int CurrentRow { get; set; }
    public int CurrentSeat { get; set; }
    public List<SelectListItem> FreeSeatsOptions { get; set; } = new();
    public string SelectedNewSeat { get; set; } = "";
}
