using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Service
{
    public interface IDiscountService
    {
        Task<Response<List<Models.Discount>>>GetAll();
        Task<Response<Models.Discount>>GetById(int id);

        Task<Response<NoContent>> Save(Models.Discount discount);
        Task<Response<NoContent>> Update(Models.Discount discount);

    }
}
