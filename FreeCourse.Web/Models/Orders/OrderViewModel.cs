using System;
using System.Collections.Generic;

namespace FreeCourse.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        //Owned Type denir bu yapıya.
        // Sipariş geçmişinde adres bilgisi gereksiz olduğundan burada yer verilmemiştir.
        //public AddressDto AddressDto { get; set; }
        public string BuyerId { get; set; }

        public List<OrderItemViewModel> orderItems;
    }
}
