namespace CinemaApp.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public string FilmTitle { get; set; }
        public DateTime StartTime { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}