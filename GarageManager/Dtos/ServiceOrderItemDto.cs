using GarageManager.Enums;

namespace GarageManager.Dtos
{
    public class ServiceOrderItemDto
    {
        public int Id { get; set; }

        public ItemType ItemType { get; set; }

        public string Name { get; set; } = "";

        public string? Brand { get; set; }

        public string? Specification { get; set; }

        public decimal Quantity { get; set; }

        public UnitType Unit { get; set; }

        public decimal UnitPrice { get; set; }

        public bool HasWarranty { get; set; }

        public int? WarrantyMonths { get; set; }

        public string? Notes { get; set; }
    }
}
