using GarageManager.Data;
using GarageManager.Enums;
using GarageManager.Models;
using GarageManager.Services;
using Microsoft.EntityFrameworkCore;

namespace GarageManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("GarageManagerDemo"));
            //builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer
            //(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ServiceOrderService>();
            builder.Services.AddScoped<ServiceOrderItemService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!db.Customers.Any())
                {
                    var customer1 = new Customer
                    {
                        FirstName = "Petar",
                        LastName = "Petrovic",
                        Phone = "061123456",
                        Email = "petar@test.com"
                    };

                    var customer2 = new Customer
                    {
                        FirstName = "Jana",
                        LastName = "Jankovic",
                        Phone = "062654321",
                        Email = "jana@test.com"
                    };

                    db.Customers.AddRange(customer1, customer2);
                    db.SaveChanges();

                    var car1 = new Car
                    {
                        Brand = "BMW",
                        Model = "E60",
                        Year = 2005,
                        CustomerId = customer1.Id
                    };

                    var car2 = new Car
                    {
                        Brand = "Audi",
                        Model = "A4",
                        Year = 2018,
                        CustomerId = customer2.Id
                    };

                    db.Cars.AddRange(car1, car2);
                    db.SaveChanges();

                    var order1 = new ServiceOrder
                    {
                        CustomerId = customer1.Id,
                        CarId = car1.Id,
                        OrderType = OrderType.ServiceAndParts,
                        Status = OrderStatus.Completed,
                        Mileage = 245000,
                        ArrivalDateTime = new DateTime(2026, 8, 10, 9, 0, 0),
                        DepartureDateTime = new DateTime(2026, 8, 10, 15, 30, 0),
                        VehicleCondition = "Lights working",
                        Notes = "Regular maintenance and oil change"
                    };

                    var order2 = new ServiceOrder
                    {
                        CustomerId = customer2.Id,
                        CarId = car2.Id,
                        OrderType = OrderType.Service,
                        Status = OrderStatus.InProgress,
                        Mileage = 142000,
                        ArrivalDateTime = new DateTime(2026, 8, 15, 10, 0, 0),
                        DepartureDateTime = null,
                        VehicleCondition = "Front brake noise reported",
                        Notes = "Inspect front braking system"
                    };

                    db.ServiceOrders.AddRange(order1, order2);
                    db.SaveChanges();

                    var items = new List<ServiceOrderItem>
                {
                    new ServiceOrderItem
                    {
                        ServiceOrderId = order1.Id,
                        ItemType = ItemType.Part,
                        Name = "Motor Oil",
                        Brand = "Motul",
                        Specification = "5W-30",
                        Quantity = 5,
                        Unit = UnitType.Liter,
                        UnitPrice = 1500,
                        HasWarranty = false,
                        Notes = "Engine oil"
                    },

                    new ServiceOrderItem
                    {
                        ServiceOrderId = order1.Id,
                        ItemType = ItemType.Part,
                        Name = "Oil Filter",
                        Brand = "Mann",
                        Specification = "HU 719/7 X",
                        Quantity = 1,
                        Unit = UnitType.Piece,
                        UnitPrice = 1200,
                        HasWarranty = true,
                        WarrantyMonths = 12
                    },

                    new ServiceOrderItem
                    {
                        ServiceOrderId = order1.Id,
                        ItemType = ItemType.Service,
                        Name = "Oil Change",
                        Quantity = 1,
                        Unit = UnitType.Hour,
                        UnitPrice = 1500,
                        HasWarranty = false,
                        Notes = "Engine oil replacement"
                    },

                    new ServiceOrderItem
                    {
                        ServiceOrderId = order2.Id,
                        ItemType = ItemType.Service,
                        Name = "Brake Inspection",
                        Quantity = 1,
                        Unit = UnitType.Hour,
                        UnitPrice = 1500,
                        HasWarranty = false
                    }
                };

                    db.ServiceOrderItems.AddRange(items);
                    db.SaveChanges();
                }
            }


            app.Run();
        }
    }
}
