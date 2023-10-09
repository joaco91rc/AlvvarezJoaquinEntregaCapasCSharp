using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public static class UsuarioData

    {

        public static Usuario ObtenerUsuario(int idUsuario)
        {
            Usuario usuarioById = new Usuario();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id, Nombre, Apellido, NombreUsuario, Contrasenia, Mail FROM Usuario where Id=@idUsuario;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    var parametro = new SqlParameter();
                    parametro.ParameterName = "idUsuario";
                    parametro.SqlDbType = SqlDbType.Int;
                    parametro.Value = idUsuario;
                    cmd.Parameters.Add(parametro);
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            usuarioById.id = Convert.ToInt32(dr["Id"]);
                            usuarioById.nombre = dr["Nombre"].ToString();
                            usuarioById.apellido = dr["Apellido"].ToString();
                            usuarioById.nombreUsuario = dr["NombreUsuario"].ToString();
                            usuarioById.contrasenia = dr["Contrasenia"].ToString();
                            usuarioById.mail = dr["Mail"].ToString();



                            
                        }
                    }

                }
                catch (Exception ex)
                {
                    
                    string mensaje = "Error en la obtencion del Usuario:" + ex;
                }

            }
            return usuarioById;
        }

        public static List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id, Nombre, Apellido, NombreUsuario, Contrasenia, Mail FROM Usuario;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                id = Convert.ToInt32(dr["Id"]),
                                nombre = dr["Nombre"].ToString(),
                                apellido = dr["Apellido"].ToString(),
                                nombreUsuario = dr["NombreUsuario"].ToString(),
                                contrasenia = dr["Contrasenia"].ToString(),
                                mail = dr["Mail"].ToString()

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Usuario>();
                    string mensaje = "Error en la obtencion de la Lista de Usuarios:" + ex;
                }

            }
            return lista;
        }

        public static void CrearUsuario(Usuario objUsuario)
        {


            try
            {
                var query = "INSERT INTO Usuario ( Nombre, Apellido, NombreUsuario, Contrasenia, Mail)" + "VALUES(@Nombre, @Apellido, @NombreUsuario, @Contrasenia, @Mail)";


                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = objUsuario.nombre });
                        cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = objUsuario.apellido });
                        cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = objUsuario.nombreUsuario });
                        cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = objUsuario.contrasenia });
                        cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = objUsuario.mail });

                        cmd.ExecuteNonQuery();
                    }




                    oconexion.Close();
                }


            }

            catch (Exception ex)
            {

                string mensaje = "Error en la insercion del Usuario:" + ex;
            }

            


        }

        public static void ModificarUsuario(Usuario objUsuario)
        {


            try
            {
                var query = "UPDATE Usuario " +
                    "SET Nombre =@Nombre, " +
                    "Apellido = @Apellido, " +
                    "NombreUsuario = @NombreUsuario, " +
                    "Contrasenia = @Contrasenia, " +
                    "Mail = @Mail " +
                    "WHERE Id=@Id";


                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objUsuario.id });
                        cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = objUsuario.nombre });
                        cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = objUsuario.apellido });
                        cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = objUsuario.nombreUsuario });
                        cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = objUsuario.contrasenia });
                        cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = objUsuario.mail });

                        cmd.ExecuteNonQuery();
                    }
                    oconexion.Close();


                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la modificacion del Usuario:" + ex;

            }




        }

        public static void EliminarUsuario(Usuario objUsuario)
        {
            var query = "DELETE FROM Usuario WHERE Id = @Id";

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {

                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objUsuario.id });
                        cmd.ExecuteNonQuery();
                    }

                    oconexion.Close();

                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la Modificacion del Usuario:" + ex;

            }



        }
    }
}
