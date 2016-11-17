using BL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class TipoCambioRepository : ITipoCambio
    { 

        public List<BancoDto> ObtenerListaBancos() 
        {
            var dbList = _context.Bancoes.Select(x => new BancoDto { id = x.id, Nombre = x.Nombre, WsIdCompra = x.WsIdCompra, WsIdVenta = x.WsIdVenta });
            return dbList.ToList();
        }

        public BancoDto ObtenerBanco(int id)
        {
            var x =  _context.Bancoes.FirstOrDefault(b => b.id == id);
            return  new BancoDto { id = x.id, Nombre = x.Nombre, WsIdCompra = x.WsIdCompra, WsIdVenta = x.WsIdVenta };
        }

    }
}
