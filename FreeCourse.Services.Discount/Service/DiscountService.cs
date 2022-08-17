using Dapper;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Service
{
    public class DiscountService : IDiscountService
    {
        // Burada class üzerinden database bağlanma işlemi yapılmamış olup farklı bir yapı ile direk IConfiguration üzerinden database'e bağlanma işlemi yapılmaktadır.
        // Tercih edilmesi gereken class üzerinden aktarma yapılarak tip güvenli bir şekilde inşa etmekdir.
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }
        public Task<Response<NoContent>> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select*from discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(),200);
        }

        public Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<Models.Discount>> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<NoContent>> Save(Models.Discount discount)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<NoContent>> Update(Models.Discount discount)
        {
            throw new System.NotImplementedException();
        }
    }
}
