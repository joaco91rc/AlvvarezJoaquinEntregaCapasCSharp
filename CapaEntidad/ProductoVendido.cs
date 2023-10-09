using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ProductoVendido
    {
        public int id { get; set; }
        public int productoId { get; set; }
        public int stock { get; set; }
        public int ventaId { get; set; }
    }
}
