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
    public static class ProductoVendidoData
    {
        public static List<ProductoVendido> ObtenerProductoVendido(int idProductoVendido)
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select pv.Id,Descripciones,pv.Stock,p.PrecioVenta,pv.IdVentafrom ProductoVendido pv inner join Producto p on pv.IdProducto = p.Id where pv.Id=@idProductoVendido;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    var parametro = new SqlParameter();
                    parametro.ParameterName = "idProductoVendido";
                    parametro.SqlDbType = SqlDbType.Int;
                    parametro.Value = idProductoVendido;
                    cmd.Parameters.Add(parametro);
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ProductoVendido()
                            {
                                id = Convert.ToInt32(dr["Id"]),
                                stock = Convert.ToInt32(dr["Stock"]),
                                productoId = Convert.ToInt32(dr["IdProducto"]),
                                ventaId = Convert.ToInt32(dr["Idventa"])
                                
                                

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ProductoVendido>();
                    string mensaje = "Error en la obtencion del ProductoVendido:" + ex;
                }

            }
            return lista;
        }

        public static List<ProductoVendido> ListarProductoVendidos()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select pv.Id,p.Descripciones,pv.Stock,p.PrecioVenta,pv.IdVenta, pv.IdProducto from ProductoVendido pv inner join Producto p on pv.IdProducto = p.Id; ");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ProductoVendido()
                            {
                                id = Convert.ToInt32(dr["Id"]),
                                stock = Convert.ToInt32(dr["Stock"]),
                                productoId = Convert.ToInt32(dr["IdProducto"]),
                                ventaId = Convert.ToInt32(dr["IdVenta"])

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ProductoVendido>();
                    string mensaje = "Error en la obtencion de la Lista de ProductoVendidos:" + ex;
                }

            }
            return lista;
        }

        public static void CrearProductoVendido(ProductoVendido objProductoVendido)
        {


            try
            {
                var query = "INSERT INTO ProductoVendido ( Stock, IdProducto, Idventa)" + "VALUES(@Stock, @IdProducto, @Idventa)";


                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.VarChar) { Value = objProductoVendido.stock });
                        cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Decimal) { Value = objProductoVendido.productoId });
                        cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Decimal) { Value = objProductoVendido.ventaId });
                        cmd.ExecuteNonQuery();
                    }




                    oconexion.Close();
                }


            }

            catch (Exception ex)
            {

                string mensaje = "Error en la insercion del ProductoVendido:" + ex;
            }




        }

        public static void ModificarProductoVendido(ProductoVendido objProductoVendido)
        {


            try
            {
                var query = "UPDATE ProductoVendido " +
                    "SET Stock =@Stock, " +
                    "IdProducto = @IdProducto, " +
                    "IdVenta = @IdVenta " +                   
                    "WHERE Id=@Id";


                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objProductoVendido.id });
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.VarChar) { Value = objProductoVendido.stock });
                        cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Decimal) { Value = objProductoVendido.productoId });
                        cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Decimal) { Value = objProductoVendido.ventaId });

                        cmd.ExecuteNonQuery();

                    }
                    oconexion.Close();


                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la modificacion del ProductoVendido:" + ex;

            }




        }

        public static void EliminarProductoVendido(ProductoVendido objProductoVendido)
        {
            var query = "DELETE FROM ProductoVendido WHERE Id = @id";

            try
            {



                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {

                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = objProductoVendido.id });
                        cmd.ExecuteNonQuery();
                    }

                    oconexion.Close();

                }

            }

            catch (Exception ex)
            {
                string mensaje = "Error en la Modificacion del ProductoVendido:" + ex;

            }



        }
    }
}
