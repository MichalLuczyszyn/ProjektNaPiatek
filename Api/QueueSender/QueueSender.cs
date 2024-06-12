using System.Text.Json;
using Api.Helpers;
using Api.Models;
using StackExchange.Redis;

namespace Api.QueueSender;

public class QueueSender : IQueueSender
{
    private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    private static IDatabase db = redis.GetDatabase();
    
    string queueName = "WorkerQueue";

    private static async Task EnqueueAsync(string queueName, Dictionary<string, string> data)
    {
        try
        {
            var json = JsonSerializer.Serialize(data);
            await db.ListRightPushAsync(queueName, json);
            Console.WriteLine($"Enqueued: {data}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public async Task Send(ReservationPdfDto data)
    {
        await EnqueueAsync(queueName, ToDictionaryExtensions.ToDictionary(data));
    }
}