using GarageManager.Dtos;

namespace GarageManager.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAll();

        Task<CustomerDto?> GetById(int id);

        Task<CustomerDto> Create(CreateCustomerDto dto);

        Task<bool> Update(int id, UpdateCustomerDto dto);

        Task<bool> Delete(int id);
        Task<CustomerDetailsDto?> GetDetails(int id);
    }
}
