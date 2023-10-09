using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        public List<Producto> ListarProductos()
        {
            return ProductoData.ListarProductos();
        }

        public static void CrearProducto(Producto objProducto)
        {
            ProductoData.CrearProducto(objProducto);
        }
        public static Producto ObtenerProducto(int idProducto)
        {
           return  ProductoData.ObtenerProducto(idProducto);
        }

        public static void ModificarProducto(Producto objProducto)
        {
            ProductoData.ModificarProducto(objProducto);
        }

        public static void EliminarProducto(Producto objProducto)
        {
            ProductoData.EliminarProducto(objProducto);
        }
    }
}
