using GarageManager.Data;
using GarageManager.Services;
using Microsoft.EntityFrameworkCore;

namespace GarageManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ICarService, CarService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
