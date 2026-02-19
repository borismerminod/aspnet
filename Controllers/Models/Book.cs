using System.ComponentModel.DataAnnotations;


namespace Controllers.Models
{
    public class Book
    {
        public int? BookID { get; set; }
        public string Author { get; set; }
        
        [Required(ErrorMessage="Bah alors tu as oublié le titre du livre sur la propriété {0}")]
        [Display(Name = "Book Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Le titre du livre ({0}) doit comporter entre {2} et {1} caractères")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "{0} n est pas bon mon petit")]
        public string BookName { get; set; }

        [Display(Name = "Prix")]
        [Range(0, 999, ErrorMessage = "{0} doit être entre {1} et {2}")]
        public Decimal Price { get; set; }


        [EmailAddress(ErrorMessage = "{0} is not valid")]
        public string AuthorEmail { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
