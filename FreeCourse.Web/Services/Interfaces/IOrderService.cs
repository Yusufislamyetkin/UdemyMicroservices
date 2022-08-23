using FreeCourse.Web.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        //Burası senkron
        // Direkt order mikroservisine istek yapacak
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput); // kurslar sepetten gelecek

        //Burası asenkron
        // Daha order tamamlanmadı kuyruğa - rabbitMQ ' ya eklenecek.
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task GetOrder(CheckoutInfoInput checkoutInfoInput);

        //Sipariş geçmişimi getirir.
        Task<List<OrderViewModel>> GetOrder();
        
    }
}
