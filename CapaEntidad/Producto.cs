using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int id { get; set; }
        public string descripciones { get; set; }
        public decimal costo { get; set; }
        public decimal precioVenta { get; set; }
        public int stock { get; set; }
        public int idUsuario { get; set; }
    }
}
