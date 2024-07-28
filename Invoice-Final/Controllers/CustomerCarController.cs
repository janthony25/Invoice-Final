using Invoice_Final.Models.Dto;
using Invoice_Final.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Final.Controllers
{
    public class CustomerCarController : Controller
    {
        private readonly ICustomerCarRepository _customerCarRepository;

        public CustomerCarController(ICustomerCarRepository customerCarRepository)
        {
            _customerCarRepository = customerCarRepository;
        }
        
        // GET - Customer Car Details
        public async Task<IActionResult> GetCustomerCars(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var customerCar = await _customerCarRepository.GetCustomerCarsListByIdAsync(id);
            return View(customerCar);
        }

        // GET - Add Car to Customer
        public async Task<IActionResult> AddCarToCustomer(int id)
        {
            var car = new AddCarToCustomerDto
            {
                CustomerId = id,
                CarRego = "",
                CarMake = ""
            };
            return View(car);
        }

        // POST - Add Car to Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCarToCustomer(AddCarToCustomerDto addCarDto)
        {
            if (ModelState.IsValid)
            {
                await _customerCarRepository.AddCarToCustomerAsync(addCarDto);
                return RedirectToAction("GetCustomerCars", new { id = addCarDto.CustomerId });
            }
            return View(addCarDto);
        }
    }
}
