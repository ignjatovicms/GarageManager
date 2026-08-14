using System;
using System.Collections.Generic;
using System.Text;
using GarageManager.Services;
using GarageManager.Data;
using Microsoft.EntityFrameworkCore;
using GarageManager.Enums;
using GarageManager.Models;
using Xunit;


namespace GarageManager.Tests
{
    public class ServiceOrderServiceTests
    {
        [Fact]
        public async Task GetById_ShouldCalculateOrderTotalsCorrectly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var order = new ServiceOrder
            {
                OrderType = OrderType.ServiceAndParts,
                Status = OrderStatus.Completed,
                Items = new List<ServiceOrderItem>
                {
                    new ServiceOrderItem
                    {
                        ItemType = ItemType.Part,
                        Name = "Motor Oil",
                        Quantity = 5,
                        Unit = UnitType.Liter,
                        UnitPrice = 1500
                    },
                    new ServiceOrderItem
                    {
                        ItemType = ItemType.Part,
                        Name = "Oil Filter",
                        Quantity = 1,
                        Unit = UnitType.Piece,
                        UnitPrice = 1200
                    },
                    new ServiceOrderItem
                    {
                        ItemType = ItemType.Service,
                        Name = "Oil Change",
                        Quantity = 1,
                        Unit = UnitType.Hour,
                        UnitPrice = 1500
                    }
                }
            };

            context.ServiceOrders.Add(order);
            await context.SaveChangesAsync();

            var service = new ServiceOrderService(context);

            // Act
            var result = await service.GetById(order.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(8700, result.PartsTotal);
            Assert.Equal(1500, result.ServicesTotal);
            Assert.Equal(10200, result.TotalPrice);
        }

        [Fact]
        public async Task GetById_ShouldCalculateDurationCorrectly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var order = new ServiceOrder
            {
                ArrivalDateTime = new DateTime(2026, 8, 11, 10, 0, 0),
                DepartureDateTime = new DateTime(2026, 8, 11, 17, 0, 0)
            };

            context.ServiceOrders.Add(order);
            await context.SaveChangesAsync();

            var service = new ServiceOrderService(context);

            // Act
            var result = await service.GetById(order.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7, result.DurationHours);
        }

        [Fact]
        public async Task GetMonthlyStatistics_ShouldCalculateStatisticsCorrectly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var order1 = new ServiceOrder
            {
                OrderType = OrderType.ServiceAndParts,
                Status = OrderStatus.Completed,
                CreatedAt = new DateTime(2026, 8, 10),
                CarId = 1,
                Items = new List<ServiceOrderItem>
        {
            new ServiceOrderItem
            {
                ItemType = ItemType.Part,
                Name = "Motor Oil",
                Quantity = 5,
                Unit = UnitType.Liter,
                UnitPrice = 1500
            },
            new ServiceOrderItem
            {
                ItemType = ItemType.Service,
                Name = "Oil Change",
                Quantity = 1,
                Unit = UnitType.Hour,
                UnitPrice = 1500
            }
        }
            };

            var order2 = new ServiceOrder
            {
                OrderType = OrderType.PartSale,
                Status = OrderStatus.Completed,
                CreatedAt = new DateTime(2026, 8, 15),
                Items = new List<ServiceOrderItem>
        {
            new ServiceOrderItem
            {
                ItemType = ItemType.Part,
                Name = "Brake Disc",
                Quantity = 2,
                Unit = UnitType.Piece,
                UnitPrice = 12000
            }
        }
            };

            context.ServiceOrders.AddRange(order1, order2);
            await context.SaveChangesAsync();

            var service = new ServiceOrderService(context);

            // Act
            var result = await service.GetMonthlyStatistics(2026, 8);

            // Assert
            Assert.Equal(2026, result.Year);
            Assert.Equal(8, result.Month);

            Assert.Equal(2, result.TotalOrders);
            Assert.Equal(2, result.CompletedOrders);

            Assert.Equal(1, result.VehicleOrders);
            Assert.Equal(1, result.PartSaleOrders);

            Assert.Equal(31500, result.PartsRevenue);
            Assert.Equal(1500, result.ServicesRevenue);
            Assert.Equal(33000, result.TotalRevenue);
        }

        [Fact]
        public async Task GetById_ShouldReturnNullDuration_WhenVehicleHasNotDeparted()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new AppDbContext(options);

            var order = new ServiceOrder
            {
                ArrivalDateTime = new DateTime(2026, 8, 14, 10, 0, 0),
                DepartureDateTime = null
            };

            context.ServiceOrders.Add(order);
            await context.SaveChangesAsync();

            var service = new ServiceOrderService(context);

            // Act
            var result = await service.GetById(order.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.DurationHours);
        }
    }
}
