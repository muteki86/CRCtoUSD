using BL;
using BL.BusinessLogic;
using BL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CambioDollar.Controllers
{
    public class CambioDolarController : ApiController
    {

        private ITipoCambio _repository;

        public CambioDolarController()
        {
            _repository = new TipoCambioRepository();
        }

        //// GET api/values
        [ActionName("getBancos")]
        [AcceptVerbs("GET")]
        public IEnumerable<BancoDto> GetBancos()
        {
            return _repository.ObtenerListaBancos();
        }

        // GET api/values/5
        public TipoCambioDto Get(int id)
        {
            var tipoCambioBl = new TipoDeCambioBl();

            // ver si banco esta en el banco
            var bancoDto = _repository.ObtenerBanco(id);
            if (bancoDto != null) 
            {
                var tipoDeCambio = _repository.ObternerUltimoTipoCambio(bancoDto.id);
                if (tipoDeCambio != null)
                {
                    if (tipoDeCambio.FechaActualizacion.Date != DateTime.Today) 
                    {
                        // si el banco esta pero la fecha es vieja: llamar al servicio, obtener el valor, actualizar bd, devolver valor
                        var nuevoTipoCambio = tipoCambioBl.ActualizarTipoCambio(bancoDto);
                        return nuevoTipoCambio;
                    }
                    // si el banco esta, y la fecha es igual a hoy: devolver el valor del cambio
                    return tipoDeCambio;
                }
                else 
                {
                    var nuevoTipoCambio = tipoCambioBl.ActualizarTipoCambio(bancoDto);
                    return nuevoTipoCambio;
                }
            }
            
            // si el banco no está: devolver null
            return null;
        }
    }
}