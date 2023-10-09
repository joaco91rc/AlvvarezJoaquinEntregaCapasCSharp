using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
   public  class CN_ProductoVendido
    {

        public List<ProductoVendido> ListarProductoVendidos()
        {
            return ProductoVendidoData.ListarProductoVendidos();
        }

        public static void CrearProductoVendido(ProductoVendido objProductoVendido)
        {
            ProductoVendidoData.CrearProductoVendido(objProductoVendido);
        }

        public static void ModificarProductoVendido(ProductoVendido objProductoVendido)
        {
            ProductoVendidoData.ModificarProductoVendido(objProductoVendido);
        }

        public static void EliminarProductoVendido(ProductoVendido objProductoVendido)
        {
            ProductoVendidoData.EliminarProductoVendido(objProductoVendido);
        }
    }
}
