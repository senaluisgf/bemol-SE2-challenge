using BemolChallenge.PaymentServiceB.Database;
using BemolChallenge.PaymentServiceB.Services;
using BemolChallenge.PaymentServiceB.Services.QuereService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<MongoDBService>();

builder.Services.AddScoped<IQueueService, QueueService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PaymentProcessor>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
