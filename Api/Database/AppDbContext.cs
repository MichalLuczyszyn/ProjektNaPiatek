using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ReservationStatus> ReservationStatuses { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<PdfFile> PdfFiles { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Report> Reports { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=carRentalDb;Username=postgres;Password=fred");
    }
}