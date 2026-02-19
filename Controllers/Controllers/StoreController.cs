using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    public class StoreController : Controller
    {
        [Route("/Category/Books")]
        public IActionResult Books()
        {
            return Content("Book page", "text/plain");
        }
    }
}
