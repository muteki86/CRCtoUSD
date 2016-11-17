using System;
using Microsoft.Azure.Mobile.Server;

namespace CRCtoUSDService.DataObjects
{
    public class TipoDeCambio : EntityData
    {
        public decimal ValorCompra { get; set; }

        public decimal ValorVenta { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}