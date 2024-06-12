namespace Api.Models;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }

    public User User { get; set; }
    public Car Car { get; set; }
}