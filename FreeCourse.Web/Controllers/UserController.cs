using FreeCourse.Web.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            // Kullanıcı bilgilerini getirir IdentityServerdan. IdentityServer'a elindeki token ile başvuruda bulunur.
            var user = await _userService.GetUser();
            return View(user);
        }
    }
}
