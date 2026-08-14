using GarageManager.Dtos;
using GarageManager.Data;
using GarageManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageManager.Services
{
    public class CustomerService : ICustomerService
    {
         private readonly AppDbContext _context;

         public CustomerService(AppDbContext context)
         {
             _context = context;
         }

         public async Task<List<CustomerDto>> GetAll()
         {
             return await _context.Customers
                 .Select(c => new CustomerDto
                 {
                     Id = c.Id,
                     FirstName = c.FirstName,
                     LastName = c.LastName,
                     Phone = c.Phone,
                     Email = c.Email
                 })
                 .ToListAsync();
         }

         public async Task<CustomerDto?> GetById(int id)
         {
             return await _context.Customers
                 .Where(c => c.Id == id)
                 .Select(c => new CustomerDto
                 {
                     Id = c.Id,
                     FirstName = c.FirstName,
                     LastName = c.LastName,
                     Phone = c.Phone,
                     Email = c.Email
                 })
                 .FirstOrDefaultAsync();
         }

         public async Task<CustomerDto> Create(CreateCustomerDto dto)
         {
             var customer = new Customer
             {
                 FirstName = dto.FirstName,
                 LastName = dto.LastName,
                 Phone = dto.Phone,
                 Email = dto.Email
             };

             _context.Customers.Add(customer);

             await _context.SaveChangesAsync();

             return new CustomerDto
             {
                 Id = customer.Id,
                 FirstName = customer.FirstName,
                 LastName = customer.LastName,
                 Phone = customer.Phone,
                 Email = customer.Email
             };
         }

         public async Task<bool> Update(int id, UpdateCustomerDto dto)
         {
             var customer = await _context.Customers.FindAsync(id);

             if (customer == null)
                 return false;

             customer.FirstName = dto.FirstName;
             customer.LastName = dto.LastName;
             customer.Phone = dto.Phone;
             customer.Email = dto.Email;

             await _context.SaveChangesAsync();

             return true;
         }

         public async Task<bool> Delete(int id)
         {
             var customer = await _context.Customers.FindAsync(id);

             if (customer == null)
                 return false;

             _context.Customers.Remove(customer);

             await _context.SaveChangesAsync();

             return true;
         }

        public async Task<CustomerDetailsDto?> GetDetails(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Cars)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return null;

            return new CustomerDetailsDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Email = customer.Email,

                Cars = customer.Cars.Select(car => new CustomerCarDto
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year
                }).ToList()
            };
        }
    }
}

