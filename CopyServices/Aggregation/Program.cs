using Aggregation.Service;
using Aggregation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddGrpc();

builder.Services.AddScoped<IStore1Service,Store1Service>();
builder.Services.AddScoped<IStoreProgramService, StoreProgramService>();

builder.Services.AddHttpClient<IStore_1Service, Store_1Service>
    (c => c.BaseAddress = new Uri("http://localhost:5263"));
builder.Services.AddHttpClient<IStoreProgram_Service, StoreProgram_Service>
    (c => c.BaseAddress = new Uri("http://localhost:5050"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGrpcService<Store1Service>();
app.MapGrpcService<StoreProgramService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
