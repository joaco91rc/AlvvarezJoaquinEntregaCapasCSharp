using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        

        public List<Usuario> ListarUsuarios()
        {
            return UsuarioData.ListarUsuarios();
        }

        public static void CrearUsuario(Usuario objUsuario)
        {
             UsuarioData.CrearUsuario(objUsuario);
        }
        public static Usuario ObtenerUsuario(int idUsuario)
        {
            return UsuarioData.ObtenerUsuario(idUsuario);
        }

        public static void ModificarUsuario(Usuario objUsuario)
        {
            UsuarioData.ModificarUsuario(objUsuario);
        }

        public static void EliminarUsuario(Usuario objUsuario)
        {
            UsuarioData.EliminarUsuario(objUsuario);
        }
    }
}
