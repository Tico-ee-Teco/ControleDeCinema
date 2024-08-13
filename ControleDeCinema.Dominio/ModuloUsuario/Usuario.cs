using Microsoft.AspNetCore.Identity;

namespace ControleDeCinema.Dominio.ModuloUsuario
{
    public class Usuario : IdentityUser<int>
    {
        public Usuario()
        {
           EmailConfirmed = true;

        }
    }
}