using Microsoft.EntityFrameworkCore;
using Proyecto_Biblioteca.Models;

namespace Proyecto_Biblioteca.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Pillara las cosas de la base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Libros> libros { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
