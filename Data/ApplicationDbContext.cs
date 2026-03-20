using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;

namespace CinemaApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}