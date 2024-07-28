using System.ComponentModel.DataAnnotations;

namespace Invoice_Final.Models.Entities
{
    public class tblCar
    {
        [Key]
        public int CarId { get; set; }
        public required string CarRego { get; set; }
        public required string CarMake { get; set; }
        public string? CarModel { get; set; }

        // FK to tblCustomer
        public int CustomerId { get; set; }
        public tblCustomer tblCustomer { get; set; }

        // many-to-many tblInvoice
        public ICollection<tblInvoice> tblInvoice { get; set; }
    }
}
