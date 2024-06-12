namespace Api.Models;

public class Report
{
    public int Id { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string FilePath { get; set; }
    public string Status { get; set; }
}