namespace WorkerService.QueueSender;

public interface IQueueSender
{
    public Task Send(int fileId);
}