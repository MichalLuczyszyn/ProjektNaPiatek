using StackExchange.Redis;

namespace WorkerService.QueueSender;

public class QueueSender : IQueueSender
{
    private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    private static IDatabase db = redis.GetDatabase();
    
    string queueName = "ApiQueue";

    private static async Task EnqueueAsync(string queueName, string message)
    {
        try
        {
            await db.ListRightPushAsync(queueName, message);
            Console.WriteLine($"Enqueued: {message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public async Task Send(int fileId)
    {
        await EnqueueAsync(queueName, fileId.ToString());
    }
}