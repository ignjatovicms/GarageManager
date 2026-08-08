using System.ComponentModel.DataAnnotations;

namespace GarageManager.Dtos
{
    public class UpdateCarDto
    {
        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = "";

        [Required]
        [Range(1900, 2040)]
        public int Year { get; set; }
    }
}
