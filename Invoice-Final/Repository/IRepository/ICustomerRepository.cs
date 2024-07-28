using Invoice_Final.Models.Dto;

namespace Invoice_Final.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task<List<CustomerSummaryDto>> GetCustomerListAsync();
        Task AddNewCustomerAsync(AddNewCustomerDto addCustomerDto);
    }
}
