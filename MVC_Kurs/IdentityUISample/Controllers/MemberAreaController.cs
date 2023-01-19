using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUISample.Controllers
{
    [Authorize]
    public class MemberAreaController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
