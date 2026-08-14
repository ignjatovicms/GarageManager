using GarageManager.Enums;

namespace GarageManager.Dtos
{
    public class ServiceOrderDto
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }

        public int? CarId { get; set; }
        public CarDto? Car { get; set; }

        public OrderType OrderType { get; set; }
        public OrderStatus Status { get; set; }

        public int? Mileage { get; set; }

        public DateTime? ArrivalDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }

        public string? VehicleCondition { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal PartsTotal { get; set; }
        public decimal ServicesTotal { get; set; }
        public double? DurationHours { get; set; }

        public List<ServiceOrderItemDto> Items { get; set; }
            = new List<ServiceOrderItemDto>();
    }
}
