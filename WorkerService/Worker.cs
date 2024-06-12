using System.Text.Json;
using StackExchange.Redis;
using WorkerService.PdfGenerator;
using WorkerService.QueueSender;

public class Worker : BackgroundService
{
    private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    private static IDatabase db = redis.GetDatabase();

    public static async Task<string> DequeueAsync(string queueName)
    {
        var message = await db.ListLeftPopAsync(queueName);
        return message;
    }
    
    private readonly ILogger<Worker> _logger;
    private readonly IQueueSender _queueSender;

    public Worker(ILogger<Worker> logger, IQueueSender queueSender)
    {
        _logger = logger;
        _queueSender = queueSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var queueName = "WorkerQueue";
        
                while (true)
                {
                    var message = await DequeueAsync(queueName);
                    var json = DeserializeDictionary(message);
                    if (!string.IsNullOrEmpty(message) && json is not null)
                    {
                        var fileId = await Generate.GenerateAndSave(json);
                        await _queueSender.Send(fileId);
                        Console.WriteLine($"Dequeued: {message}");
                    }
                    else
                    {
                        Console.WriteLine("Queue is empty");
                        await Task.Delay(1000, stoppingToken); // Wait for a second before checking again
                    }
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private static Dictionary<string, string>? DeserializeDictionary(string message)
    {
        try
        {
            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(message);
            return json;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }
}