using BL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BusinessLogic
{
    public class TipoDeCambioBl
    {
        private ITipoCambio _repository;

        public TipoDeCambioBl()
        {
            _repository = new TipoCambioRepository();
        }

        public TipoCambioDto ActualizarTipoCambio(BancoDto bancoDto)
        {
            
            var servicio = new BancoCentral.wsIndicadoresEconomicosSoapClient();
            var valorVentaDts = servicio.ObtenerIndicadoresEconomicos("318", DateTime.Today.ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "Francisco Rodriguez", "N");
            var valorVenta = Convert.ToDecimal(valorVentaDts.Tables[0].Rows[0].ItemArray[2]);

            var valorCompraDts = servicio.ObtenerIndicadoresEconomicos("317", DateTime.Today.ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "Francisco Rodriguez", "N");
            var valorCompra = Convert.ToDecimal(valorCompraDts.Tables[0].Rows[0].ItemArray[2]);

            var nuevoTipoCambio = new TipoCambioDto
            {
                FechaActualizacion = DateTime.Today,
                idBanco = bancoDto.id,
                ValorCompra = valorCompra,
                ValorVenta = valorVenta
            };

            _repository.CrearTipoCambio(nuevoTipoCambio);
            return nuevoTipoCambio;

        }
    }
}
