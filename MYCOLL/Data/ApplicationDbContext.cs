using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MYCOLL.Entities;
using MYCOLL.Entities.Users;

namespace MYCOLL.Data // Define os mapeamentos das classes c# para a base de dados
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
		// Domain entities
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<SubCategoria> SubCategorias { get; set; }
		public DbSet<Produto> Produtos { get; set; }

		// Orders / cart
		public DbSet<Encomenda> Encomendas { get; set; }
		public DbSet<ItemCarrinho> ItensCarrinho { get; set; }

		// Users / actors
		public DbSet<Fornecedor> Fornecedores { get; set; }
		public DbSet<Funcionario> Funcionarios { get; set; }
		public DbSet<Utilizador> Utilizadores { get; set; }

		// Payments
		public DbSet<Pagamento> Pagamentos { get; set; }


	}
}
