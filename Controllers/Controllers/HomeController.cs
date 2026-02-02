using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    //Quand on ajoute le suffixe Controller à une classe, ASP.NET Core la reconnait automatiquement comme un contrôleur MVC.
    //[Controller] //On peut utiliser cette annotation pour retirer le suffixe Controller du nom de la classe
    public class HomeController
    {
        [Route("Home")]
        [Route("/")]
        public string Index()
        {
            return "Welcome to ASP.NET Core MVC!";
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
    }
}
