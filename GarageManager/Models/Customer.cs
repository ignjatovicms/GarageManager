namespace GarageManager.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public ICollection<ServiceOrder> ServiceOrders { get; set; } 
                = new List<ServiceOrder>();
    }
}
