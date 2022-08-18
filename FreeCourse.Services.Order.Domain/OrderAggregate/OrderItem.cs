using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem
    {
        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public string ProductId { get; private set; }  //CourseId için ProductId isimlendirmesini tercih ettik.
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public decimal Price { get; private set; }

        // Geleneksel mimaride orderId ye de ihtiyaç duyardık burada. Fakat bu yapıda order, orderıtem ile birlikte çalışmak mecburiyetinde kalır anladığım kadarıyla ikisi tek bir yapıyı oluşturur.
        // nosql veritabanı yapılarındaki gibi

        // ilişkisel yapı için iç tarafta orderıd bu yapıda tamımlanır. Buna da shadow property denir.

        public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
        {
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;
        }
    }
}
