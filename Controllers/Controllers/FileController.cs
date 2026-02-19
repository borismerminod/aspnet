using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    public class FileController : Controller
    {
        //Récupère une image dans wwwroot
        [Route("File/DowloadFile")]
        //public VirtualFileResult FileDownload1()
        public IActionResult FileDownload1()
        {
            //return new VirtualFileResult("/example.png", "image/png");
            return File("/example.png", "image/png");
        }

        //Récupère un fichier dans un autre endroit que le wwwroot
        [Route("File/DowloadFile2/{filename}")]
        //public PhysicalFileResult FileDownload2()
        public IActionResult FileDownload2(string filename)
        {
            var path = $@"G:\_Bobo\MateusBubono\Courant\Projet\aspnet\fichiersexemples\{filename}";
            if (System.IO.File.Exists(path) == false)
            {
                return Content("File not found", "text/plain");
            }
           
            return new PhysicalFileResult(path, "image/png");
          
        }

        [Route("File/DowloadFile3")]
        //public FileContentResult FileDownload3()
        public IActionResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"G:\_Bobo\MateusBubono\Courant\Projet\aspnet\fichiersexemples\example2.txt");
            //return new FileContentResult(bytes, "test/plain"); 
            return File(bytes, "test/plain"); 

        }


    }
}
