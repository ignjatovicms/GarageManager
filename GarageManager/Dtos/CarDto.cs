namespace GarageManager.Dtos
{
    public class CarDto // Car Data Transfer Object
    {   
        public int Id { get; set; }
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int Year { get; set; }
    }
}
