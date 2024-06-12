using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using WorkerService.Database;
using WorkerService.Models;

namespace WorkerService.PdfGenerator
{
    public class Generate
    {
        private static async Task GeneratePdf(Stream outputStream, Dictionary<string, string> data)
        {
            await using var writer = new PdfWriter(outputStream);
            using var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            foreach (var kvp in data)
            {
                document.Add(new Paragraph($"{kvp.Key}: {kvp.Value}"));
            }

            document.Close();
        }

        public static async Task<int> GenerateAndSave(Dictionary<string, string> data)
        {
            using var pdfStream = new MemoryStream();
            await GeneratePdf(pdfStream, data);

            var pdfBinary = pdfStream.ToArray();
            var id = await SavePdfToDatabase("output.pdf", pdfBinary);

            return id;
        }
        
        private static async Task<int> SavePdfToDatabase(string fileName, byte[] fileData)
        {
            await using var context = new AppDbContext();
            var pdfFile = new PdfFile
            {
                FileName = fileName,
                FileData = fileData
            };

            context.PdfFiles.Add(pdfFile);
            await context.SaveChangesAsync();

            Console.WriteLine("PDF saved to database.");

            return pdfFile.Id;
        }
    }
}