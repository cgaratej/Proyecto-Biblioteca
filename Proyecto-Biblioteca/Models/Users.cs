using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Biblioteca.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El User es obligatorio")]
        [Display(Name = "Usuario")]
        public string name { get; set; }
        [Required(ErrorMessage = "La Password es obligatoria")]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Display(Name = "Rango")]
        public int rango { get; set; }
        [NotMapped]
        [Display(Name = "isAdmin")]
        public bool isAdmin { get; set; }
        [NotMapped]
        [Display(Name = "isChange")]
        public bool isPassChage { get; set; }
        [NotMapped]
        [Display(Name = "Confirm Password")]
        public string confirmPassword { get; set; }
    }
}
