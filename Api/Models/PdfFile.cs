namespace Api.Models;

public class PdfFile
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
}