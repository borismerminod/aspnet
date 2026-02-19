using Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    public class BookController : Controller
    {
        [Route("/Books")]
        public IActionResult Book()
        {
            if(Request.Query.ContainsKey("BookId") == false)
            {
                //Response.StatusCode = 400;
                //return Content("BookId query parameter is required", "text/plain");
                return BadRequest("BookId query parameter is required");
            }

            int bookId = Convert.ToInt32(Request.Query["BookId"]);
            if(bookId < 1 || bookId > 1000)
            {
                return NotFound("Book not found");
            }

            if(Convert.ToBoolean(Request.Query["IsLogged"]) == false)
            {
                return Unauthorized("You are not logged");
            }

            return File("/example.png", "image/png");
        }

        [Route("/Books2")]
        public IActionResult Book(int bookId, string author)
        {
            return Content($"BookId: {bookId}, Author: {author}", "text/plain");
        }

        [Route("/Books3/{bookId}/{author}")]
        public IActionResult Book3(int bookId, string author)
        {
            return Content($"BookId: {bookId}, Author: {author}", "text/plain");
        }

        [Route("/Books4/{bookId}/{author}")]
        public IActionResult Book4([FromRoute] int? bookId, [FromQuery] string author)
        {
            if(bookId.HasValue == false)
            {
                return Content("Book ID not provided", "text/plain");
            }
            return Content($"BookId: {bookId}, Author: {author}", "text/plain");
        }

        //priorité de taitement : Form data > request body > route parameter > query string
        //Les form data sont de deux types : 
        //  - x-www-form-urlencoded => quantité de données limitée et doit être plus simple
        //  - form-data => quantité de données plus importantes, plus complexe (comme un fichier par exemple)
        [Route("/BookModel/{BookID?}/{Author?}")]
        public IActionResult BookModel(Book book)
        {
            if (book.BookID.HasValue == false)
            {
                return Content("Book ID not provided", "text/plain");
            }

            /*if(string.IsNullOrEmpty(book.BookName))
            {
                return BadRequest("Book name is a required field");
            }*/

            //On test ici si book contient des valeur valide par rapport à ce qui a été annoté dans la classe modèle
            if(ModelState.IsValid == false)
            {
                List<string> errors = new List<string>();
                //Contient l'ensemble des erreurs de validation
                foreach(var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorString = "Nombre d'erreur : "+ModelState.ErrorCount + " "+string.Join("\n", errors);
                return BadRequest(errorString);
            }

            return Json(book); //Content($"BookId: {book.BookID}, Author: {book.Author}", "text/plain");
        }

        [Route("/BookModel2/{BookID?}/{Author?}")]
        public IActionResult BookModel2([FromQuery] Book book)
        {
            if (book.BookID.HasValue == false)
            {
                return Content("Book ID not provided", "text/plain");
            }
            return Content($"BookId: {book.BookID}, Author: {book.Author}", "text/plain");
        }

        [Route("/BookModel3/{BookID?}/{Author?}")]
        public IActionResult BookModel3(Book2 book)
        {
            if (book.BookID.HasValue == false)
            {
                return Content("Book ID not provided", "text/plain");
            }
            return Content($"BookId: {book.BookID}, Author: {book.Author}", "text/plain");
        }



        [Route("/OldBooks")]
        public IActionResult OldBook()
        {
            //Code 301 - la page à été bougée de manière permanente
            //Code 302 - Found la page à  été bougée temporairement
            //return RedirectToAction("Books", "Store", new {}); //Redirige vers la méthode Books du StoreController
            //return new RedirectToActionResult("Books", "Store", new {}, permanent: true); //Redirige vers la méthode Books du StoreController - envoi le code 301 avec le booléen à vrai en troisieme param
            return RedirectToActionPermanent("Books", "Store", new {}); //Redirige vers la méthode Books du StoreController - envoi le code 301 avec le booléen à vrai en troisieme param
        }
    }
}
