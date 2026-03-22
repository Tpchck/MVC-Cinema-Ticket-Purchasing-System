namespace CinemaTicketSystem.Models;

public class AdminDashboardViewModel
{
    public IEnumerable<User> Users { get; set; } = new List<User>();
    public IEnumerable<Screening> Screenings { get; set; } = new List<Screening>();
}
