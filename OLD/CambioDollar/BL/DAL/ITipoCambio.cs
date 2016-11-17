using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Objects;

namespace BL
{
    public interface ITipoCambio
    {
        List<BancoDto> ObtenerListaBancos();
        BancoDto ObtenerBanco(int id);


        TipoCambioDto ObternerUltimoTipoCambio(int idBanco);
        void CrearTipoCambio(TipoCambioDto tipoCambio);

    }
}
