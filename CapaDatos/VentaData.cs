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
    public static class VentaData
    {
        public static Venta ObtenerVenta(int idVenta)
        {
            Venta ventaById = new Venta();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.Id, u.NombreUsuario,v.IdUsuario, v.Comentarios from Venta v inner join Usuario u on v.IdUsuario=u.Id where Id=@idVenta;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    var parametro = new SqlParameter();
                    parametro.ParameterName = "idVenta";
                    parametro.SqlDbType = SqlDbType.Int;
                    parametro.Value = idVenta;
                    cmd.Parameters.Add(parametro);
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            
                            ventaById.id = Convert.ToInt32(dr["Id"]);
                            ventaById.comentarios = dr["Stock"].ToString();
                            ventaById.idUsuario = Convert.ToInt32(dr["IdProducto"]);




                         
                        }
                    }

                }
                catch (Exception ex)
                {
                    
                    string mensaje = "Error en la obtencion de la Venta:" + ex;
                }

            }
            return ventaById;
        }

        public static List<Venta> ListarVentas()
        {
            List<Venta> lista = new List<Venta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id, Comentarios, IdUsuario FROM Venta;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Venta()
                            {
                                id = Convert.ToInt32(dr["Id"]),
                                comentarios = dr["Comentarios"].ToString(),
                                idUsuario = Convert.ToInt32(dr["IdUsuario"])

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Venta>();
                    string mensaje = "Error en la obtencion de la Lista de Ventas:" + ex;
                }

            }
            return lista;
        }

        public static void CrearVenta(Venta objVenta)
        {


            try
            {
                var query = "INSERT INTO Venta ( Comentarios, IdUsuario)" + "VALUES(@Comentarios, @IdUsuario)";


                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = objVenta.comentarios });
                        cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Decimal) { Value = objVenta.idUsuario });
                        cmd.ExecuteNonQuery();

                    }




                    oconexion.Close();
                }


            }

            catch (Exception ex)
            {

                string mensaje = "Error en la insercion del Venta:" + ex;
            }




        }

        public static void ModificarVenta(Venta objVenta)
        {


            try
            {
                var query = "UPDATE Venta " +
                    "SET Comentarios = @Comentarios, " +
                    "IdUsuario = @IdUsuario " +                    
                    "WHERE Id=@Id";


                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objVenta.id });
                        cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = objVenta.comentarios });
                        cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int) { Value = objVenta.idUsuario });
                        cmd.ExecuteNonQuery();


                    }
                    oconexion.Close();


                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la modificacion del Venta:" + ex;

            }




        }

        public static void EliminarVenta(Venta objVenta)
        {
            var query = "DELETE FROM Venta WHERE Id = @id";

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {

                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objVenta.id });
                        cmd.ExecuteNonQuery();
                    }

                    oconexion.Close();

                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la Modificacion del Venta:" + ex;

            }



        }

    }
}
