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
    public static class ProductoData
    {
        public static Producto ObtenerProducto(int idProducto)
        {
            Producto productoById = new Producto();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id, Descripciones, Costo, PrecioVenta, Stock, idUsuario FROM Producto where Id=@idProducto;");
                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    var parametro = new SqlParameter();
                    parametro.ParameterName = "idProducto";
                    parametro.SqlDbType = SqlDbType.Int;
                    parametro.Value = idProducto;
                    cmd.Parameters.Add(parametro);
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            productoById.id = Convert.ToInt32(dr["Id"]);
                            productoById.descripciones = dr["Descripciones"].ToString();
                            productoById.costo = Convert.ToDecimal(dr["Costo"]);
                            productoById.precioVenta = Convert.ToDecimal(dr["PrecioVenta"]);
                            productoById.stock = Convert.ToInt32(dr["Stock"]);
                            productoById.idUsuario = Convert.ToInt32(dr["IdUsuario"]);
                                
                            
                        }
                    }

                }
                catch (Exception ex)
                {
                    productoById = new Producto();
                    string mensaje = "Error en la obtencion del Producto:" + ex;
                }

            }
            return productoById;
        }

        public static List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id, Descripciones, Costo, PrecioVenta, Stock, idUsuario FROM Producto;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                id = Convert.ToInt32(dr["Id"]),
                                descripciones = dr["Descripciones"].ToString(),
                                costo = Convert.ToDecimal(dr["Costo"]),
                                precioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                stock = Convert.ToInt32(dr["Stock"]),
                                idUsuario = Convert.ToInt32(dr["IdUsuario"])

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                    string mensaje = "Error en la obtencion de la Lista de Productos:" + ex;
                }

            }
            return lista;
        }

        public static void CrearProducto(Producto objProducto)
        {
            

            try
            {
                var query = "INSERT INTO Producto (Descripciones,Costo,Precioventa,Stock,idusuario)" + "VALUES(@Descripciones,@Costo,@Precioventa,@Stock,@idUsuario)";

                
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = objProducto.descripciones });
                        cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Decimal) { Value = objProducto.costo.ToString("0.00") });
                        cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Decimal) { Value = objProducto.precioVenta.ToString("0.00") });
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = objProducto.stock });
                        cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = objProducto.idUsuario });
                        cmd.ExecuteNonQuery();
                    }
                    



                    oconexion.Close();
                }

                
            }

            catch (Exception ex)
            {

                string mensaje = "Error en la insercion del Producto:" + ex;
            }


            

        }

        public static void ModificarProducto(Producto objProducto)
        {
            

            try
            {
                var query = "UPDATE Producto " +
                    "SET Descripciones =@Descripciones, " +
                    "Costo = @Costo, " +
                    "Precioventa = @PrecioVenta, " +
                    "Stock = @Stock, " +
                    "IdUsuario = @idUsuario " +
                    "WHERE Id=@Id";


                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objProducto.id });
                        cmd.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = objProducto.descripciones });
                        cmd.Parameters.Add(new SqlParameter("Costo", SqlDbType.Decimal) { Value = objProducto.costo });
                        cmd.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Decimal) { Value = objProducto.precioVenta });
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = objProducto.stock });
                        cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = objProducto.idUsuario });
                        cmd.ExecuteNonQuery();

                    }
                    oconexion.Close();


                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la modificacion del Producto:" + ex;
                
            }


        

        }

        public static void EliminarProducto(Producto objProducto)
        {
            var query = "DELETE FROM Producto WHERE Id = @Id";

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {

                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objProducto.id });
                        cmd.ExecuteNonQuery();
                    }

                    oconexion.Close();

                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la Modificacion del Producto:" + ex;

            }



        }

    }
    }

