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
        public TipoCambioDto ObternerUltimoTipoCambio(int idBanco) 
        {
            var tipoCambio = _context.TipoCambioDiarios.Where(t => t.idBanco == idBanco).OrderBy(o => o.FechaActualizacion).FirstOrDefault();
            if (tipoCambio != null)
            {
                return new TipoCambioDto
                {
                    id = tipoCambio.id,
                    FechaActualizacion = tipoCambio.FechaActualizacion,
                    idBanco = tipoCambio.idBanco,
                    ValorCompra = tipoCambio.ValorCompra,
                    ValorVenta = tipoCambio.ValorVenta
                };
            }
            return null;
        }

        public void CrearTipoCambio(TipoCambioDto tipoCambio)
        {
            _context.TipoCambioDiarios.Add(new DAL.TipoCambioDiario
            {
                idBanco = tipoCambio.idBanco,
                FechaActualizacion = tipoCambio.FechaActualizacion,
                ValorCompra = tipoCambio.ValorCompra,
                ValorVenta = tipoCambio.ValorVenta
            });
            _context.SaveChanges();
        }

    }
}
