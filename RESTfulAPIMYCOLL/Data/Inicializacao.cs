using Microsoft.AspNetCore.Identity;
using RESTfulAPIMYCOLL.Entities;

namespace RESTfulAPIMYCOLL.Data
{
    public class Inicializacao
    {
        public static async Task CriaDadosIniciais(
        UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Adicionar default Roles
            var roles = TipoUtilizador.GetAllTipos();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    IdentityRole roleRole = new IdentityRole(role);
                    await roleManager.CreateAsync(roleRole);
                }
            }
            
        }
    }
}
