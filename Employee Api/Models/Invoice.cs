namespace Employee_Api.Models
{
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public string CustomerName { get; set;}
        public string CustomerEmail { get; set;}
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set;}
    }
}
