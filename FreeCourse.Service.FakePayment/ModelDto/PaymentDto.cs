namespace FreeCourse.Service.FakePayment.ModelDto
{
    // Payment service'de bu dto ile direkt olarak message sistemine 
    public class PaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderDto Order { get; set; }
    }
}
