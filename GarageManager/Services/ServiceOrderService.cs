using GarageManager.Data;
using GarageManager.Dtos;
using GarageManager.Enums;
using GarageManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageManager.Services
{
    public class ServiceOrderService
    {
        private readonly AppDbContext _context;

        public ServiceOrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceOrderDto> Create(CreateServiceOrderDto dto)
        {
            if (dto.CustomerId.HasValue)
            {
                var customerExists = await _context.Customers
                    .AnyAsync(c => c.Id == dto.CustomerId.Value);

                if (!customerExists)
                    throw new ArgumentException("Customer does not exist.");
            }

            if (dto.CarId.HasValue)
            {
                var car = await _context.Cars
                    .FirstOrDefaultAsync(c => c.Id == dto.CarId.Value);

                if (car == null)
                    throw new ArgumentException("Car does not exist.");

                if (dto.CustomerId.HasValue && car.CustomerId != dto.CustomerId.Value)
                    throw new ArgumentException("Car does not belong to the selected customer.");
            }

            var order = new ServiceOrder
            {
                CustomerId = dto.CustomerId,
                CarId = dto.CarId,
                OrderType = dto.OrderType,
                Mileage = dto.Mileage,
                ArrivalDateTime = dto.ArrivalDateTime,
                VehicleCondition = dto.VehicleCondition,
                Notes = dto.Notes
            };

            foreach (var itemDto in dto.Items)
            {
                var item = new ServiceOrderItem
                {
                    ItemType = itemDto.ItemType,
                    Name = itemDto.Name,
                    Brand = itemDto.Brand,
                    Specification = itemDto.Specification,
                    Quantity = itemDto.Quantity,
                    Unit = itemDto.Unit,
                    UnitPrice = itemDto.UnitPrice,
                    HasWarranty = itemDto.HasWarranty,
                    WarrantyMonths = itemDto.WarrantyMonths,
                    Notes = itemDto.Notes
                };

                order.Items.Add(item);
            }

            _context.ServiceOrders.Add(order);

            await _context.SaveChangesAsync();

            return new ServiceOrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CarId = order.CarId,
                OrderType = order.OrderType,
                Status = order.Status,
                Mileage = order.Mileage,
                ArrivalDateTime = order.ArrivalDateTime,
                VehicleCondition = order.VehicleCondition,
                Notes = order.Notes,
                CreatedAt = order.CreatedAt,

                Items = order.Items.Select(item => new ServiceOrderItemDto
                {
                    Id = item.Id,
                    ItemType = item.ItemType,
                    Name = item.Name,
                    Brand = item.Brand,
                    Specification = item.Specification,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    UnitPrice = item.UnitPrice,
                    HasWarranty = item.HasWarranty,
                    WarrantyMonths = item.WarrantyMonths,
                    Notes = item.Notes
                }).ToList()
            };
        }

        public async Task<List<ServiceOrderDto>> GetAll()
        {
            return await _context.ServiceOrders
                .Include(o => o.Customer)
                .Include(o => o.Car)
                .Include(o => o.Items)
                .Select(o => new ServiceOrderDto
                {
                    Id = o.Id,

                    CustomerId = o.CustomerId,
                    CarId = o.CarId,

                    OrderType = o.OrderType,
                    Status = o.Status,

                    Mileage = o.Mileage,

                    ArrivalDateTime = o.ArrivalDateTime,
                    DepartureDateTime = o.DepartureDateTime,

                    VehicleCondition = o.VehicleCondition,
                    Notes = o.Notes,
                    CreatedAt = o.CreatedAt,

                    PartsTotal = o.Items
                        .Where(i => i.ItemType == ItemType.Part)
                        .Sum(i => i.Quantity * i.UnitPrice),

                    ServicesTotal = o.Items
                        .Where(i => i.ItemType == ItemType.Service)
                        .Sum(i => i.Quantity * i.UnitPrice),

                    TotalPrice = o.Items
                .        Sum(i => i.Quantity * i.UnitPrice),

                    DurationHours = o.ArrivalDateTime.HasValue &&
                            o.DepartureDateTime.HasValue
                    ? (o.DepartureDateTime.Value - o.ArrivalDateTime.Value).TotalHours
                    : null,

                    Customer = o.Customer == null ? null : new CustomerDto
                    {
                        Id = o.Customer.Id,
                        FirstName = o.Customer.FirstName,
                        LastName = o.Customer.LastName,
                        Phone = o.Customer.Phone,
                        Email = o.Customer.Email
                    },

                    Car = o.Car == null ? null : new CarDto
                    {
                        Id = o.Car.Id,
                        Brand = o.Car.Brand,
                        Model = o.Car.Model,
                        Year = o.Car.Year,
                        CustomerId = o.Car.CustomerId
                    },

                    Items = o.Items.Select(item => new ServiceOrderItemDto
                    {
                        Id = item.Id,
                        ItemType = item.ItemType,
                        Name = item.Name,
                        Brand = item.Brand,
                        Specification = item.Specification,
                        Quantity = item.Quantity,
                        Unit = item.Unit,
                        UnitPrice = item.UnitPrice,
                        HasWarranty = item.HasWarranty,
                        WarrantyMonths = item.WarrantyMonths,
                        Notes = item.Notes
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ServiceOrderDto?> GetById(int id)
        {
            var order = await _context.ServiceOrders
                .Include(o => o.Customer)
                .Include(o => o.Car)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return null;

            return new ServiceOrderDto
            {
                Id = order.Id,

                CustomerId = order.CustomerId,
                CarId = order.CarId,

                OrderType = order.OrderType,
                Status = order.Status,

                Mileage = order.Mileage,

                ArrivalDateTime = order.ArrivalDateTime,
                DepartureDateTime = order.DepartureDateTime,

                VehicleCondition = order.VehicleCondition,
                Notes = order.Notes,
                CreatedAt = order.CreatedAt,
                PartsTotal = order.Items
                                  .Where(i => i.ItemType == ItemType.Part)
                                  .Sum(i => i.Quantity * i.UnitPrice),
                ServicesTotal = order.Items
                                     .Where(i => i.ItemType == ItemType.Service)
                                     .Sum(i => i.Quantity * i.UnitPrice),
                TotalPrice = order.Items.Sum(i => i.Quantity * i.UnitPrice),
                DurationHours = order.ArrivalDateTime.HasValue &&
                                order.DepartureDateTime.HasValue
                ? (order.DepartureDateTime.Value - order.ArrivalDateTime.Value)
                                .TotalHours
                                : null,

                Customer = order.Customer == null ? null : new CustomerDto
                {
                    Id = order.Customer.Id,
                    FirstName = order.Customer.FirstName,
                    LastName = order.Customer.LastName,
                    Phone = order.Customer.Phone,
                    Email = order.Customer.Email
                },

                Car = order.Car == null ? null : new CarDto
                {
                    Id = order.Car.Id,
                    Brand = order.Car.Brand,
                    Model = order.Car.Model,
                    Year = order.Car.Year,
                    CustomerId = order.Car.CustomerId
                },

                Items = order.Items.Select(item => new ServiceOrderItemDto
                {
                    Id = item.Id,
                    ItemType = item.ItemType,
                    Name = item.Name,
                    Brand = item.Brand,
                    Specification = item.Specification,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    UnitPrice = item.UnitPrice,
                    HasWarranty = item.HasWarranty,
                    WarrantyMonths = item.WarrantyMonths,
                    Notes = item.Notes
                }).ToList()
            };
        }

        public async Task<ServiceOrderDto?> Update(int id, UpdateServiceOrderDto dto)
        {
            var order = await _context.ServiceOrders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return null;

            if (dto.CustomerId.HasValue)
            {
                var customerExists = await _context.Customers
                    .AnyAsync(c => c.Id == dto.CustomerId.Value);

                if (!customerExists)
                    throw new ArgumentException("Customer does not exist.");
            }

            if (dto.CarId.HasValue)
            {
                var car = await _context.Cars
                    .FirstOrDefaultAsync(c => c.Id == dto.CarId.Value);

                if (car == null)
                    throw new ArgumentException("Car does not exist.");

                if (dto.CustomerId.HasValue &&
                    car.CustomerId != dto.CustomerId.Value)
                {
                    throw new ArgumentException(
                        "Car does not belong to the selected customer.");
                }
            }

            order.CustomerId = dto.CustomerId;
            order.CarId = dto.CarId;
            order.OrderType = dto.OrderType;
            order.Status = dto.Status;
            order.Mileage = dto.Mileage;
            order.ArrivalDateTime = dto.ArrivalDateTime;
            order.DepartureDateTime = dto.DepartureDateTime;
            order.VehicleCondition = dto.VehicleCondition;
            order.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return await GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _context.ServiceOrders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return false;

            _context.ServiceOrders.Remove(order);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<MonthlyStatisticsDto> GetMonthlyStatistics(int year, int month)
        {
            var orders = await _context.ServiceOrders
                .Include(o => o.Items)
                .Where(o => o.CreatedAt.Year == year &&
                            o.CreatedAt.Month == month)
                .ToListAsync();

            return new MonthlyStatisticsDto
            {
                Year = year,
                Month = month,

                TotalOrders = orders.Count,

                CompletedOrders = orders.Count(o =>
                    o.Status == OrderStatus.Completed),

                VehicleOrders = orders.Count(o =>
                    o.CarId.HasValue),

                PartSaleOrders = orders.Count(o =>
                    o.OrderType == OrderType.PartSale),

                PartsRevenue = orders
                    .SelectMany(o => o.Items)
                    .Where(i => i.ItemType == ItemType.Part)
                    .Sum(i => i.Quantity * i.UnitPrice),

                ServicesRevenue = orders
                    .SelectMany(o => o.Items)
                    .Where(i => i.ItemType == ItemType.Service)
                    .Sum(i => i.Quantity * i.UnitPrice),

                TotalRevenue = orders
                    .SelectMany(o => o.Items)
                    .Sum(i => i.Quantity * i.UnitPrice)
            };
        }
    }
}
