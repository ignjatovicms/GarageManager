using System.ComponentModel.DataAnnotations;
using GarageManager.Enums;

namespace GarageManager.Dtos
{
    public class CreateServiceOrderItemDto
    {
        [Required]
        public ItemType ItemType { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [StringLength(50)]
        public string? Brand { get; set; }

        [StringLength(100)]
        public string? Specification { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Required]
        public UnitType Unit { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public bool HasWarranty { get; set; }

        [Range(1, 120)]
        public int? WarrantyMonths { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}