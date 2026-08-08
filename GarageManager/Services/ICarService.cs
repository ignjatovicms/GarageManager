using GarageManager.Dtos;

namespace GarageManager.Services
{
    public interface ICarService
    {
        Task<List<CarDto>> GetAll();
        Task<CarDto?> GetById(int id);
        Task<CarDto> Create(CreateCarDto dto);
        Task<bool> Update(int id, UpdateCarDto dto);
        Task<bool> Delete(int id);
        Task<List<CarDto>> Search(string brand);
    }
}
