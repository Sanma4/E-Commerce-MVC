using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public int TotalProductos { get; set; }
        public decimal MontoTotal{ get; set; }
        public string Contacto { get; set; }
        public int IdDistrito { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int IdTransaccion { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}
