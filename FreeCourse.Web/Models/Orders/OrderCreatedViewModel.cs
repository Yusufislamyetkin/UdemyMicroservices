﻿namespace FreeCourse.Web.Models.Orders
{
    public class OrderCreatedViewModel
    {
        // Oluşturulan işlemden geri gelen orderId
        public int OrderId { get; set; }

        public string Error { get; set; }
        public bool IsSuccessful { get; set; }

    }
}
