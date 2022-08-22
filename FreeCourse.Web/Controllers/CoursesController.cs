using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var ıd = _sharedIdentityService.GetUserId;
            var value = await _catalogService.GetlAllCourseByUserIdAsync(ıd);


            return View(value);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetlAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
   
            if (!ModelState.IsValid)
            {
                return View();
            }
            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

           var value =  await _catalogService.AddCourseAsync(courseCreateInput);

            return RedirectToAction(nameof(Index));
        }
    }
}
