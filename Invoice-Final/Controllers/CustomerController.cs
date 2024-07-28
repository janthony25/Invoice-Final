using Invoice_Final.Models.Dto;
using Invoice_Final.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Final.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET - Customer List
        public async Task<IActionResult> GetCustomerList()
        {
            var customer = await _customerRepository.GetCustomerListAsync();
            return View(customer);
        }

        // GET - Add New Customer
        public async Task<IActionResult> AddNewCustomer()
        {
            return View();
        }

        // POST - Add New Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewCustomer(AddNewCustomerDto addCustomerDto)
        {
            if(ModelState.IsValid)
            {
                await _customerRepository.AddNewCustomerAsync(addCustomerDto);
                return RedirectToAction("GetCustomerList");
            }
            return View(addCustomerDto);
        }
    }
}
