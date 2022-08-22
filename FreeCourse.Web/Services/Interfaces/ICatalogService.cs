using FreeCourse.Web.Models.Catalogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        // İlk önce tüm kursları alalım.
        Task<List<CourseViewModel>> GetAllCourse();
        Task<List<CourseViewModel>> GetlAllCourseByUserIdAsync(string userId);
        Task<List<CategoryViewModel>> GetlAllCategoryAsync();
      
        Task<CourseViewModel> GetCourseByIdAsync(string courseId);
        Task<bool> DeleteCourseAsync(string courseId);
        Task<bool> AddCourseAsync(CourseCreateInput  courseCreateInput);
        Task<bool> UpdateCourseAsync(CourseUpdateInput  courseUpdateInput);



    }
}
