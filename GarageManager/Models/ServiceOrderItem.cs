using GarageManager.Enums;

namespace GarageManager.Models
{
    public class ServiceOrderItem
    {
        public int Id { get; set; }

        public int ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; } = null!;

        // PART or SERVICE
        public ItemType ItemType { get; set; }

        public string Name { get; set; } = "";

        public string? Brand { get; set; }

        public string? Specification { get; set; }

        public decimal Quantity { get; set; }

        // PCS, LITER, HOUR, SERVICE...
        public UnitType Unit { get; set; }

        public decimal UnitPrice { get; set; }

        public bool HasWarranty { get; set; }

        public int? WarrantyMonths { get; set; }

        public string? Notes { get; set; }
    }
}
