using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUISample.Controllers
{
    public class MixController : Controller
    {
        private UserManager<IdentityUser> _userManager;

        public MixController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }



        [AllowAnonymous]
        public IActionResult Index()
        {
            //_userManager.AddToRolesAsync(User, );
            return View();
        }

        [Authorize(Roles ="Admins")]
        public IActionResult Index2()
        {
            return View();
        }
    }
}
