using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Biblioteca.Models
{
    public class Libros
    {
        //Elemtos que tandra la tabal de la base de datos
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El name es obligatorio")]
        [Display(Name = "Nombre")]
        public string name { get; set; }
        [Required(ErrorMessage = "El autor es obligatorio")]
        [Display(Name = "Autor")]
        public string autor { get; set; }
        [Required(ErrorMessage = "La descrpción es obligatorio")]
        [Display(Name = "Descripcion")]
        public string description { get; set; }
        [Display(Name = "Video Url")]
        public string linkVideo { get; set; }
        public string UrlPdf { get; set; }
        [NotMapped]
        [Display(Name = "Pdf Libro")]
        public IFormFile File { get; set; }
    }
}
