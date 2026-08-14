using GarageManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace GarageManager.Dtos
{
    public class UpdateServiceOrderDto
    {
        [Range(1, int.MaxValue)]
        public int? CustomerId { get; set; }

        [Range(1, int.MaxValue)]
        public int? CarId { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Range(0, int.MaxValue)]
        public int? Mileage { get; set; }

        public DateTime? ArrivalDateTime { get; set; }

        public DateTime? DepartureDateTime { get; set; }

        [StringLength(1000)]
        public string? VehicleCondition { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }
    }
}
