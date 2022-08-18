using AutoMapper;
using FreeCourse.Services.Order.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Mapping
{
    //Class library içerisinde bir startup dosyası olmadığı için dependency ınjection yapamamaktayız bundan mütevellit bu yöntemlerle mapping yapabilmekteyiz.
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Order.Domain.OrderAggregate.Order, OrderDto>();
            CreateMap<Order.Domain.OrderAggregate.OrderItem, OrderItemDto>();
            CreateMap<Order.Domain.OrderAggregate.Address, AddressDto>();
        }
    }
}
