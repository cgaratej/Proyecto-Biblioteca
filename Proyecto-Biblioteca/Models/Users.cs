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
        public int rango { get; set; }
        [NotMapped]
        [Display(Name = "isAdmin")]
        public bool isAdmin { get; set; }
    }
}
