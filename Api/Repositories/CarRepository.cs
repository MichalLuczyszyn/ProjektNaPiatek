using Api.Database;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await _context.Cars
            .Include(c => c.ReservationStatus)
            .Include(c => c.Reservations)
            .ToListAsync();
    }

    public async Task<Car> GetByIdAsync(int id)
    {
        return await _context.Cars
            .Include(c => c.ReservationStatus)
            .Include(c => c.Reservations)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}