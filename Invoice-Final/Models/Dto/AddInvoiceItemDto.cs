﻿namespace Invoice_Final.Models.Dto
{
    public class AddInvoiceItemDto
    {
        public required string ItemName { get; set; }
        public required int Quantity { get; set; }
        public required decimal ItemPrice { get; set; }
    }
}