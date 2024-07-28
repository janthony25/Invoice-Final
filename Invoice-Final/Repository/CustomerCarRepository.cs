using Invoice_Final.Data;
using Invoice_Final.Models.Dto;
using Invoice_Final.Models.Entities;
using Invoice_Final.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Final.Repository
{
    public class CustomerCarRepository : ICustomerCarRepository
    {
        private readonly DataContext _data;
        public CustomerCarRepository(DataContext data)
        {
            _data = data;   
        }

        public async Task AddCarToCustomerAsync(AddCarToCustomerDto addCarDto)
        {
            var customer = await _data.tblCustomer
                .Include(c => c.tblCar)
                .FirstOrDefaultAsync(c => c.CustomerId == addCarDto.CustomerId);

            if (customer != null)
            {
                var newCar = new tblCar
                {
                    CarRego = addCarDto.CarRego,
                    CarMake = addCarDto.CarMake,
                    CarModel = addCarDto.CarModel
                };

                customer.tblCar.Add(newCar);
                await _data.SaveChangesAsync();
            }
        }

        public async Task<List<CustomerCarSummaryDto>> GetCustomerCarsListByIdAsync(int id)
        {
            return await _data.tblCustomer
                .Include(c => c.tblCar)
                .Where(c => c.CustomerId == id)
                .SelectMany(c => c.tblCar.DefaultIfEmpty(), (customer, car) => new CustomerCarSummaryDto // will retrieve customer even if car is empty
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    CustomerEmail = customer.CustomerEmail,
                    CustomerNumber = customer.CustomerNumber,
                    PaymentStatus = customer.PaymentStatus,
                    CarId = car != null ? car.CarId : 0,
                    CarRego = car != null ? car.CarRego : string.Empty,
                    CarMake = car != null ? car.CarMake : string.Empty,
                    CarModel = car != null ? car.CarModel : string.Empty
                }).ToListAsync();

        }
    }
}
