using Api.Models;

namespace Api.QueueSender;

public interface IQueueSender
{
    public Task Send(ReservationPdfDto data);
}