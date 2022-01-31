using Microsoft.AspNetCore.Server.Kestrel.Core;
using TicTacToeService.Controllers;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();


//i maed dis
BaseController mainController = new BaseController(configuration["MongoDB:Server"], configuration["MongoDB:Database"]);
builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(5000));


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
