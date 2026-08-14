using GarageManager.Enums;

namespace GarageManager.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }

        // Customer is optional
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Car is optional
        public int? CarId { get; set; }
        public Car? Car { get; set; }

        // Type of order: Service, PartSale, ServiceAndParts
        public OrderType OrderType { get; set; }

        // Vehicle information
        public int? Mileage { get; set; }

        // Arrival / departure
        public DateTime? ArrivalDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }

        public string? VehicleCondition { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Open;

        public ICollection<ServiceOrderItem> Items { get; set; }
            = new List<ServiceOrderItem>();
    }
}
