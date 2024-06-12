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
    
    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { nameof(PricePerDay), PricePerDay.ToString() },
            { nameof(Id), Id.ToString() },
            { nameof(Make), Make },
            { nameof(Model), Model },
            { nameof(Status), Status },
            { nameof(Year), Year.ToString() },
            { nameof(CarId), CarId.ToString() },
            { nameof(EndDate), EndDate.ToString("yyyy-MM-dd HH:mm:ss") },
            { nameof(StartDate), StartDate.ToString("yyyy-MM-dd HH:mm:ss") },
            { nameof(LicensePlate), LicensePlate },
            { nameof(StatusId), StatusId.ToString() },
            { nameof(UserId), UserId.ToString() }
        };
    }
}