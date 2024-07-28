namespace Invoice_Final.Models.Dto
{
    public class AddCarToCustomerDto
    {
        public int CustomerId { get; set; }
        public required string CarRego { get; set; }
        public required string CarMake { get; set; }
        public string? CarModel { get; set; }
    }
}
