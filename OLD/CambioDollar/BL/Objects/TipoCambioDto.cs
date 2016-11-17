using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Objects
{
    public class TipoCambioDto
    {
        public int id { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenta { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int idBanco { get; set; }

    }
}
