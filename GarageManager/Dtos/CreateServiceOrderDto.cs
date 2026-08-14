using GarageManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace GarageManager.Dtos
{
    public class CreateServiceOrderDto
    {
        [Range(1, int.MaxValue)]
        public int? CustomerId { get; set; }

        [Range(1, int.MaxValue)]
        public int? CarId { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        [Range(0, int.MaxValue)]
        public int? Mileage { get; set; }

        public DateTime? ArrivalDateTime { get; set; }

        [StringLength(1000)]
        public string? VehicleCondition { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }

        public List<CreateServiceOrderItemDto> Items { get; set; }
            = new List<CreateServiceOrderItemDto>();
    }
}
