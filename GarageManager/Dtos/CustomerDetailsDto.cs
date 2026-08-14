namespace GarageManager.Dtos
{
    public class CustomerDetailsDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";

        public List<CustomerCarDto> Cars { get; set; } = new();
    }
}
