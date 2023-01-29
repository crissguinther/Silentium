using Microsoft.AspNetCore.Mvc;

namespace Silentium.WebAPI.Controllers {
    public class UserController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
