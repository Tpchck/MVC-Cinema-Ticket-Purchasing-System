using Microsoft.EntityFrameworkCore;
using CinemaTicketSystem.Models;

namespace CinemaTicketSystem.Data;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u=>u.Email).IsUnique();

        modelBuilder.Entity<Cinema>().HasData(
            new Cinema { Id = 1, Name = "Room 1", Rows = 15, SeatsPerRow = 20 },
            new Cinema { Id = 2, Name = "Room 2 (VIP)", Rows = 5, SeatsPerRow = 8 },
            new Cinema { Id = 3, Name = "Room 3 (5D VIP)", Rows = 10, SeatsPerRow = 15 }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 9000, 
                FirstName = "Admin",
                LastName = "",
                PhoneNumber = "+0000000000",
                Email = "admin@cinema.com",
                PasswordHash = "admin123",
                Role = "Admin"
            }
        );
    }
}