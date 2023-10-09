using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        public List<Venta> ListarVentas()
        {
            return VentaData.ListarVentas();
        }

        public static void CrearVenta(Venta objVenta)
        {
            VentaData.CrearVenta(objVenta);
        }

        public static void ModificarVenta(Venta objVenta)
        {
            VentaData.ModificarVenta(objVenta);
        }

        public static void EliminarVenta(Venta objVenta)
        {
            VentaData.EliminarVenta(objVenta);
        }
    }
}
