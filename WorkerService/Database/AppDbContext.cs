using Microsoft.EntityFrameworkCore;
using WorkerService.Models;

namespace WorkerService.Database;

public class AppDbContext : DbContext
{
    public DbSet<PdfFile> PdfFiles { get; set; } 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=carRentalDb;Username=postgres;Password=fred");
    }
}