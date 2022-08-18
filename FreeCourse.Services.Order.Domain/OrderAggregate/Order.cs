using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //EFCORE features

    // - Owned Types
    // - Shadow Property
    // - Backing Field

    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; set; }

        //Owned Type denir bu yapıya.
        public Address Address { get; set; }
        public string BuyerId { get; set; }

        // Private olarak belirliyoruz ki kimse direkt olarak erişemesin ve direkt olarak orderıtem ekleyemesin.Backing fields olarak isimlendirilir
        private readonly List<OrderItem> _orderItems;
        private IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(Address address, string buyerId)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;

        }
        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
