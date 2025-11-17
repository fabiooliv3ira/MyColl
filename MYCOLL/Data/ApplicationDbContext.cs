using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MYCOLL.Data // Define os mapeamentos das classes c# para a base de dados
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<MYCOLL.Entities.Categoria> Categorias { get; set; }
        public DbSet<MYCOLL.Entities.Produtos> Produtos { get; set; }
        public DbSet<MYCOLL.Entities.ModoEntrega> ModosEntrega { get; set; }
        
    }
}
