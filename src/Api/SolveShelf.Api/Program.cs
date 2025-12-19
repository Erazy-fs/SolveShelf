using SolveShelf.Api.Kafka;
using SolveShelf.Infrastructure.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSolveShelfKafka(builder.Configuration);
builder.Services.AddSingleton<ISubmissionQueueProducer, SubmissionQueueProducer>();

builder.Services.AddSingleton<IResultsStore, InMemoryResultsStore>();
builder.Services.AddHostedService<KafkaResultConsumer>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();
