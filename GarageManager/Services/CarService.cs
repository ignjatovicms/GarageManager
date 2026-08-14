using GarageManager.Data;
using GarageManager.Dtos;
using GarageManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace GarageManager.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<CarDto> Create(CreateCarDto dto)
        {
            var customerExists = await _context.Customers
            .AnyAsync(c => c.Id == dto.CustomerId);

            if (!customerExists)
            {
                throw new ArgumentException("Customer does not exist.");
            }
            var car = new Car
            {
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                CustomerId = dto.CustomerId
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                CustomerId =car.CustomerId
            };
        }

        public async Task<bool> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<List<CarDto>> GetAll()
        {
            return await _context.Cars
                .Select(c => new CarDto 
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    CustomerId = c.CustomerId,

                    Customer = new CustomerDto
                    {
                        Id = c.Customer.Id,
                        FirstName = c.Customer.FirstName,
                        LastName = c.Customer.LastName,
                        Phone = c.Customer.Phone,
                        Email = c.Customer.Email
                    }
                }).ToListAsync();
        }

        public async Task<CarDto?> GetById(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
                return null;

            return new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                CustomerId = car.CustomerId,

                Customer = new CustomerDto
                {
                    Id = car.Customer.Id,
                    FirstName = car.Customer.FirstName,
                    LastName = car.Customer.LastName,
                    Phone = car.Customer.Phone,
                    Email = car.Customer.Email
                }
            };
        }

        public async Task<List<CarDto>> Search(string brand)
        {
            return await _context.Cars
                .Where(c => c.Brand == brand)
                .Select(c => new CarDto
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    CustomerId = c.CustomerId
                })
                .ToListAsync();
        }

        public async Task<bool> Update(int id, UpdateCarDto dto)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return false;
            }

            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.Year = dto.Year;

            await _context.SaveChangesAsync();

            return true;
        }

       
    }
}
