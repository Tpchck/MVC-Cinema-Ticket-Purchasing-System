namespace CinemaTicketSystem.Models;

public class SeatMapViewModel
{
    public Screening Screening { get; set; } = null!;
    public List<Reservation> ExistingReservations { get; set; } = new List<Reservation>();
    public int CurrentUserId { get; set; }
}
