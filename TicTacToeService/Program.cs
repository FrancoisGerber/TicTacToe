using Microsoft.AspNetCore.Server.Kestrel.Core;
using TicTacToeService.Controllers;

//i maed dis
BaseController mainController = new BaseController("mongodb://localhost:27017", "TicTacToe");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//i maed dis
builder.Services.Configure<KestrelServerOptions>(options =>
   {
       options.AllowSynchronousIO = true;
   });


builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//i maed dis
app.UseCors(
       options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
   );

app.Run();
