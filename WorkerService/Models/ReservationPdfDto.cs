namespace Api.Models;

public class ReservationPdfDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    
    //car
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; }
    public int StatusId { get; set; }
    public decimal PricePerDay { get; set; }
}