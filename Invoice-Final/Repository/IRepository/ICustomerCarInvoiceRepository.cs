using Invoice_Final.Models.Dto;

namespace Invoice_Final.Repository.IRepository
{
    public interface ICustomerCarInvoiceRepository
    {
        Task<List<CustomerCarInvoiceSummaryDto>> GetCutomerCarInvoiceSummaryAsync(int id);
        Task AddCustomerCarInvoiceAsync(AddCustomerCarInvoiceDto addCarInvoiceDto);
    }
}
