using Api.Helpers;
using Api.Models;
using Api.QueueSender;
using StackExchange.Redis;

namespace Api.QueueReceiver;

public class Worker : BackgroundService
{
    private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    private static IDatabase db = redis.GetDatabase();

    public static async Task<object?> DequeueAsync(string queueName)
    {
        try
        {
            var message = await db.ListLeftPopAsync(queueName);
            return message;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
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
                var queueName = "ApiQueue";
        
                while (true)
                {
                    await Task.Delay(1000, stoppingToken);
                    var request = new ReservationPdfDto()
                    {
                        PricePerDay = 10,
                        Id = 1,
                        Make = "Volvo",
                        Model = "Bituś",
                        Status = "Nowy",
                        Year = 2019,
                        CarId = 30,
                        EndDate = new DateTime(2024, 7, 12, 1, 1, 1),
                        StartDate = new DateTime(2024, 6, 12, 1, 1, 1),
                        LicensePlate = "RLU Bitus",
                        StatusId = 40,
                        UserId = 10
                    };
                    await _queueSender.Send(request);
                    var message = await DequeueAsync(queueName);
                    if (message is not null)
                    {
                        Console.WriteLine($"Dequeued: {message}");
                    }
                    else
                    {
                        Console.WriteLine("Queue is empty");
                        await Task.Delay(10000, stoppingToken); // Wait for a second before checking again
                    }
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
