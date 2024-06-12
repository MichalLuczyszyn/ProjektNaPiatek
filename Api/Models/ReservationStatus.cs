namespace Api.Models;

public class ReservationStatus
{
    public int Id { get; set; }
    public string Description { get; set; }

    public ICollection<Car> Cars { get; set; }
}