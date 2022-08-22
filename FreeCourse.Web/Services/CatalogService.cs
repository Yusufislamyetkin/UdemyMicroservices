using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<bool> AddCourseAsync(CourseCreateInput courseCreateInput)
        {
            var response = await _client.PostAsJsonAsync<CourseCreateInput>("courses/Create", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _client.DeleteAsync($"courses/Delete/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CourseViewModel>> GetAllCourse()
        {
            //var resposne = _client.GetFromJsonAsync<CourseViewModel>(_client.BaseAddress);

            //Aşşağıdakinin meaili :  http:localhost:5000/services/catalog/courses
            var response = await _client.GetAsync("courses/GetAll");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data;

        }

        public async Task<CourseViewModel> GetCourseByIdAsync(string courseId)
        {
            var response = await _client.GetAsync($"courses/GetById/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return responseSuccess.Data;
        }

        public async Task<List<CategoryViewModel>> GetlAllCategoryAsync()
        {
            var response = await _client.GetAsync("categories/GetAll");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetlAllCourseByUserIdAsync(string userId)
        {
            var response = await _client.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {

            var response = await _client.PutAsJsonAsync<CourseUpdateInput>("courses/Update", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
