using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _meditor;
        private readonly ISharedIdentityService sharedIdentityService;

        public OrdersController(IMediator meditor, ISharedIdentityService sharedIdentityService)
        {
            _meditor = meditor;
            this.sharedIdentityService = sharedIdentityService;
        }
    }
}
