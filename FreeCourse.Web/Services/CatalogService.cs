using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    // Catalog ve PhotoStock API lar sadece client ıd ve client secret ile işlem yapabilmekte.
    //  IdentityService'den veri alabilmek için User service alanında http üzerinden cookie bilgilerinde yer alan
    //  oturum açmış kullanıcının verileri ile token gönderme işlemi yaptık.
    // Catalog içinse ya InMemmory'de yer alan client ıd secret ile ya da Redis üzerinde yer alan client ıd ve secret ile 
    // İstek yaparken header'a token eklemesi yapacağız. Bu token eklemesini her service için ayrı ayrı yapmaktansa Handler üzerinden gerçekleştireceğiz.

    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public Task<bool> AddCourseAsync(CourseCreateInput courseCreateInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCourseAsync(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCourse()
        {
            throw new System.NotImplementedException();
        }

        public Task<CourseViewModel> GetCourseByIdAsync(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CategoryViewModel>> GetlAllCategoryAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetlAllCourseByUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
