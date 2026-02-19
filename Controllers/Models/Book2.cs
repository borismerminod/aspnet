using Microsoft.AspNetCore.Mvc;

namespace Controllers.Models
{
    public class Book2
    {
        [FromRoute]
        public int? BookID { get; set; }
        [FromQuery]
        public string Author { get; set; }
    }
}
