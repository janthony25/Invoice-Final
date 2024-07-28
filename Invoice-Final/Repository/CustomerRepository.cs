using Invoice_Final.Data;
using Invoice_Final.Models.Dto;
using Invoice_Final.Models.Entities;
using Invoice_Final.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Final.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _data;
        public CustomerRepository(DataContext data)
        {
            _data = data;
        }

        public async Task AddNewCustomerAsync(AddNewCustomerDto addCustomerDto)
        {
            var customer = new tblCustomer
            {
                CustomerName = addCustomerDto.CustomerName,
                CustomerEmail = addCustomerDto.CustomerEmail,
                CustomerNumber = addCustomerDto.CustomerNumber
            };

            _data.AddAsync(customer);
            await _data.SaveChangesAsync();
        }

        public async Task<List<CustomerSummaryDto>> GetCustomerListAsync()
        {
            return await _data
                .tblCustomer
                .Select(c => new CustomerSummaryDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerNumber = c.CustomerNumber,
                    CustomerEmail = c.CustomerEmail
                }).ToListAsync();
        }
    }
}
