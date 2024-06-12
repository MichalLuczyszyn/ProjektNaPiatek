using WorkerService.Database;
using WorkerService.QueueSender;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IQueueSender, QueueSender>();
builder.Services.AddDbContext<AppDbContext>();

var host = builder.Build();
host.Run();