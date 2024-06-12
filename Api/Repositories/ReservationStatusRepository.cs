using Api.Database;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ReservationStatusRepository : IReservationStatusRepository
{
    private readonly AppDbContext _context;

    public ReservationStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReservationStatus>> GetAllAsync()
    {
        return await _context.ReservationStatuses
            .Include(rs => rs.Cars)
            .ToListAsync();
    }

    public async Task<ReservationStatus> GetByIdAsync(int id)
    {
        return await _context.ReservationStatuses
            .Include(rs => rs.Cars)
            .FirstOrDefaultAsync(rs => rs.Id == id);
    }

    public async Task AddAsync(ReservationStatus reservationStatus)
    {
        await _context.ReservationStatuses.AddAsync(reservationStatus);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ReservationStatus reservationStatus)
    {
        _context.ReservationStatuses.Update(reservationStatus);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var reservationStatus = await _context.ReservationStatuses.FindAsync(id);
        if (reservationStatus != null)
        {
            _context.ReservationStatuses.Remove(reservationStatus);
            await _context.SaveChangesAsync();
        }
    }
}