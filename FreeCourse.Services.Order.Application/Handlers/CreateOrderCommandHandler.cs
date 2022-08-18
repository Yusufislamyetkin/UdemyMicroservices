using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    // CQRS Yapımızın Command kısmında yani write kısmında çalışıyoruz.
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        // Veritabanı bağlantımızı yaptık istersek repository ile de çalışabilirdik. Böylesi daha kolay ve pratik diye tercih ettik.
        private OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        // CancellationToken işlem iptali sırasında hata fırlatır ve işlemin devam etmesini engeller.
        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Siparişimizi oluşturmak için ilk olarak gönderilen requestteki adress kısmını dolduruyoruz. Bu kısım order ile direkt bağlı olduğundan
            // direkt ordercreate kısmında inşa ediyoruz.
            var newAddress = new Address(request.AddressDto.Province, request.AddressDto.District, request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);

            //Requestten gelen adresi doldurduktan sonra requestten gelen BuyerId ile , yeni adresimizi siparişimize ekliyoruz. 
            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            // Sıra geldi siparişimizdeki itemleri(ürünleri eklemede) 
            // requestin içersindeki ürünleri teker teker özniteliklerle siparişimizin içine ekliyoruz.
            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            // en sonunda da içerisine ürünler dolan, satın alanı ve adresi belli olan siparişimizi veritabanına kaydediyoruz.
            _context.Orders.Add(newOrder);
            var result = await _context.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}
