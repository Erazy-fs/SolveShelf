using SolveShelf.Infrastructure.Kafka;
using SolveShelf.Runner;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSolveShelfKafka(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
