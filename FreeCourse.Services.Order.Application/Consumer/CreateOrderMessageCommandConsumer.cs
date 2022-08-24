using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Consumer
{
    //internal class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    //{
    //    private readonly OrderDbContext _orderDbContext;

    //    public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
    //    {
    //        _orderDbContext = orderDbContext;
    //    }

    //    public Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    //    {
    //      var newAddress = new Domain.OrderAggregate.Address(context.Message.Province)
    //    }
    //}
}
