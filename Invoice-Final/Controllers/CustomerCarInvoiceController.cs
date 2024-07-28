using Invoice_Final.Models.Dto;
using Invoice_Final.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Final.Controllers
{
    public class CustomerCarInvoiceController : Controller
    {
        private readonly ICustomerCarInvoiceRepository _customerCarInvoiceRepository;
        public CustomerCarInvoiceController(ICustomerCarInvoiceRepository customerCarInvoiceRepository)
        {
            _customerCarInvoiceRepository = customerCarInvoiceRepository;
        }
        
        // GET - Customer Car Invoice Summary
        public async Task<IActionResult> GetCustomerInvoiceSummary(int id)
        {
            var customerInvoice = await _customerCarInvoiceRepository.GetCutomerCarInvoiceSummaryAsync(id);
            return View(customerInvoice);
        }

        // GET - Add Invoice to Car
        public async Task<IActionResult> AddInvoiceToCar(int id)
        {

        }
    }
}
