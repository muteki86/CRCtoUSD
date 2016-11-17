using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Objects
{
    public class BancoDto
    {
        public int id { get; set; }
        public string Nombre { get; set; }
            public string WsIdCompra { get; set; }
        public string WsIdVenta { get; set; }
    }
}
