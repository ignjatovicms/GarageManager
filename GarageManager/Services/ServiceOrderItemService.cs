using GarageManager.Data;
using GarageManager.Dtos;
using GarageManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageManager.Services
{
    public class ServiceOrderItemService
    {
        private readonly AppDbContext _context;

        public ServiceOrderItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceOrderItemDto?> Create(
            int serviceOrderId,
            CreateServiceOrderItemDto dto)
        {
            var serviceOrder = await _context.ServiceOrders
                .FirstOrDefaultAsync(o => o.Id == serviceOrderId);

            if (serviceOrder == null)
                return null;

            var item = new ServiceOrderItem
            {
                ServiceOrderId = serviceOrderId,
                ItemType = dto.ItemType,
                Name = dto.Name,
                Brand = dto.Brand,
                Specification = dto.Specification,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
                UnitPrice = dto.UnitPrice,
                HasWarranty = dto.HasWarranty,
                WarrantyMonths = dto.WarrantyMonths,
                Notes = dto.Notes
            };

            _context.ServiceOrderItems.Add(item);

            await _context.SaveChangesAsync();

            return new ServiceOrderItemDto
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
            };
        }

        public async Task<List<ServiceOrderItemDto>> GetAll(int serviceOrderId)
        {
            return await _context.ServiceOrderItems
                .Where(i => i.ServiceOrderId == serviceOrderId)
                .Select(i => new ServiceOrderItemDto
                {
                    Id = i.Id,
                    ItemType = i.ItemType,
                    Name = i.Name,
                    Brand = i.Brand,
                    Specification = i.Specification,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    UnitPrice = i.UnitPrice,
                    HasWarranty = i.HasWarranty,
                    WarrantyMonths = i.WarrantyMonths,
                    Notes = i.Notes
                })
                .ToListAsync();
        }

        public async Task<ServiceOrderItemDto?> GetById(int serviceOrderId, int itemId)
        {
                var item = await _context.ServiceOrderItems
                .FirstOrDefaultAsync(i =>
                    i.Id == itemId &&
                    i.ServiceOrderId == serviceOrderId);

            if (item == null)
                return null;

            return new ServiceOrderItemDto
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
            };
        }

        public async Task<ServiceOrderItemDto?> Update(
                int serviceOrderId,
                int itemId,
                CreateServiceOrderItemDto dto)
        {
            var item = await _context.ServiceOrderItems
                .FirstOrDefaultAsync(i =>
                    i.Id == itemId &&
                    i.ServiceOrderId == serviceOrderId);

            if (item == null)
                return null;

            item.ItemType = dto.ItemType;
            item.Name = dto.Name;
            item.Brand = dto.Brand;
            item.Specification = dto.Specification;
            item.Quantity = dto.Quantity;
            item.Unit = dto.Unit;
            item.UnitPrice = dto.UnitPrice;
            item.HasWarranty = dto.HasWarranty;
            item.WarrantyMonths = dto.WarrantyMonths;
            item.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return new ServiceOrderItemDto
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
            };
        }

        public async Task<bool> Delete(int serviceOrderId, int itemId)
        {
                var item = await _context.ServiceOrderItems
                .FirstOrDefaultAsync(i =>
                    i.Id == itemId &&
                    i.ServiceOrderId == serviceOrderId);

            if (item == null)
                return false;

            _context.ServiceOrderItems.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }



    }
}
