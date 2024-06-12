using Api.Models;

namespace Api.Repositories;

public interface IReservationStatusRepository
{
    Task<IEnumerable<ReservationStatus>> GetAllAsync();
    Task<ReservationStatus> GetByIdAsync(int id);
    Task AddAsync(ReservationStatus reservationStatus);
    Task UpdateAsync(ReservationStatus reservationStatus);
    Task DeleteAsync(int id);
}