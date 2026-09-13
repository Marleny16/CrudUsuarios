using CrudUsuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudUsuarios.Data
{
    /// <summary>
    /// Acceso a datos del CRUD. Usa una base de datos EN MEMORIA para que el
    /// proyecto corra sin instalar SQL Server. Si prefieren SQL Server/SQLite,
    /// solo cambien el registro del DbContext en Program.cs.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
