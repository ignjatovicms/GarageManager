namespace GarageManager.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int Year { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<ServiceOrder> ServiceOrders { get; set; }
                   = new List<ServiceOrder>();
    }
}

