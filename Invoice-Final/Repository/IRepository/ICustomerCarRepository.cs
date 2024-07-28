using Invoice_Final.Models.Dto;

namespace Invoice_Final.Repository.IRepository
{
    public interface ICustomerCarRepository
    {
        Task<List<CustomerCarSummaryDto>> GetCustomerCarsListByIdAsync(int id);
        Task AddCarToCustomerAsync(AddCarToCustomerDto addCarDto);
    }
}
