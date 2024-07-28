using Invoice_Final.Data;
using Invoice_Final.Models.Dto;
using Invoice_Final.Models.Entities;
using Invoice_Final.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Final.Repository
{
    public class CustomerCarInvoiceRepository : ICustomerCarInvoiceRepository
    {
        private readonly DataContext _dataContext;
        public CustomerCarInvoiceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddCustomerCarInvoiceAsync(AddCustomerCarInvoiceDto addCarInvoiceDto)
        {
            var customer = await _dataContext.tblCar
                .Include(car => car.tblInvoice)
                .FirstOrDefaultAsync(car => car.CarId == addCarInvoiceDto.CarId);

            var invoice = new tblInvoice
            {
                DateAdded = addCarInvoiceDto.DateAdded,
                DueDate = addCarInvoiceDto.DueDate,
                IssueName = addCarInvoiceDto.IssueName,
                PaymentTerm = addCarInvoiceDto.PaymentTerm,
                Notes = addCarInvoiceDto.Notes,
                LaborPrice = addCarInvoiceDto.LaborPrice,
                Discount = addCarInvoiceDto.Discount,
                SubTotal = addCarInvoiceDto.SubTotal,
                TaxAmount = addCarInvoiceDto.TaxAmount,
                TotalAmount = addCarInvoiceDto.TotalAmount,
                AmountPaid = addCarInvoiceDto.AmountPaid,
                CarId = customer.CarId
            };

            _dataContext.tblInvoice.Add(invoice);
            await _dataContext.SaveChangesAsync();

            var invoiceItems = addCarInvoiceDto.InvoiceItems.Select(item => new tblInvoiceItem
            {
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                ItemPrice = item.ItemPrice
            }).ToList();

            _dataContext.tblInvoiceItem.AddRange(invoiceItems);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<CustomerCarInvoiceSummaryDto>> GetCutomerCarInvoiceSummaryAsync(int id)
        {
            return await _dataContext.tblCar
                .Include(car => car.tblCustomer)
                .Include(car => car.tblInvoice)
                .Where(car => car.CarId == id)
                .SelectMany(car => car.tblInvoice.DefaultIfEmpty(), (car, invoice) => new CustomerCarInvoiceSummaryDto
                {
                    CustomerId = car.tblCustomer.CustomerId,
                    CustomerName = car.tblCustomer.CustomerName,
                    CustomerEmail = car.tblCustomer.CustomerEmail,
                    CustomerNumber = car.tblCustomer.CustomerNumber,
                    CarId = car.CarId,
                    CarRego = car.CarRego,
                    CarMake = car.CarMake,
                    CarModel = car.CarModel,
                    InvoiceId = invoice != null ? invoice.InvoiceId : 0,
                    DateAdded = invoice.DateAdded,
                    DueDate = invoice.DueDate,
                    IssueName = invoice.IssueName,
                    TotalAmount = invoice.TotalAmount,
                    AmountPaid = invoice.AmountPaid
                }).ToListAsync();

        }
    }
}
