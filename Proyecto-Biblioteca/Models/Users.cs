using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Biblioteca.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El User es obligatorio")]
        [Display(Name = "User")]
        public string name { get; set; }
        [Required(ErrorMessage = "La Password es obligatoria")]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "El Rango es obligatorio")]
        [Display(Name = "Rango")]
        /*1 = Creador de usurios
          2 = Creador de Libros y categorias
          3 = Administrador*/
        public int rango { get; set; }

    }
}
