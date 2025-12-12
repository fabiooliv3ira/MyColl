using System;
using Microsoft.AspNetCore.Identity;
using MYCOLL.Entities;
using System.ComponentModel.DataAnnotations;

namespace MYCOLL.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        public string apelido { get; set; } = string.Empty;

        public string? NIF { get; set; } // Útil para Fornecedores e Clientes

        public DateTime DataRegisto { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = false;

        // Relação inversa: Um Fornecedor pode ter muitos Produtos
       // public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }

}


