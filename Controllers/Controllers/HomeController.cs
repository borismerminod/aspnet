using Microsoft.AspNetCore.Mvc;
using Controllers.Models;

namespace Controllers.Controllers
{
    //Quand on ajoute le suffixe Controller à une classe, ASP.NET Core la reconnait automatiquement comme un contrôleur MVC.
    //[Controller] //On peut utiliser cette annotation pour retirer le suffixe Controller du nom de la classe
    public class HomeController : Controller
    {
        [Route("Home")]
        [Route("/")]
        //public ContentResult Index()
        public IActionResult Index()
        {
            /*return new ContentResult()
            {
                Content = "<h1>Welcome to Home Page !</h1>",
                //ContentType = "text/plain"
                ContentType = "text/html"
            };*/

            return Content("<h1>Welcome to Home Page !</h1>", "text/html");
        }

        [Route("About")]
        public string About()
        {
            return "You are in about page";
        }

        [Route("Contact")]
        public string Contact()
        {
            return "You are in contact page";
        }
        

        [Route("/products/{id:int:min(1000):max(9999)}")]
        public string Products()
        {
            return "Products page !";
        }

        [Route("/Employee/John")]
        //public JsonResult Employee()
        public IActionResult Employee()
        {

            Employee emp = new Employee() { Age = 30, ID = 1, Name = "John", Salary = 5000 };
            

            return Json(emp);
        }
    }
}
