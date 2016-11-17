using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using CRCtoUSDService.DataObjects;
using CRCtoUSDService.Models;

namespace CRCtoUSDService.Controllers
{
    public class TipoDeCambioController : TableController<TipoDeCambio>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CRCtoUSDContext context = new CRCtoUSDContext();
            DomainManager = new EntityDomainManager<TipoDeCambio>(context, Request);
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<TipoDeCambio> GetTipoDeCambio()
        {
            var result = Query().OrderByDescending(x => x.FechaActualizacion).FirstOrDefault();
                
            if (result != null && result.FechaActualizacion != DateTime.Today)
            {
                var servicio = new BancoCentral.wsIndicadoresEconomicosSoapClient();
                var valorVentaDts = servicio.ObtenerIndicadoresEconomicos("318", DateTime.Today.ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "Francisco Rodriguez", "N");
                var valorVenta = Convert.ToDecimal(valorVentaDts.Tables[0].Rows[0].ItemArray[2]);

                var valorCompraDts = servicio.ObtenerIndicadoresEconomicos("317", DateTime.Today.ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy"), "Francisco Rodriguez", "N");
                var valorCompra = Convert.ToDecimal(valorCompraDts.Tables[0].Rows[0].ItemArray[2]);

                var nuevoTipoCambio = new TipoDeCambio
                {
                    FechaActualizacion = DateTime.Today,
                    ValorCompra = valorCompra,
                    ValorVenta = valorVenta
                };

                result = await InsertAsync(nuevoTipoCambio);
            }

            return result;
        }
    }
}