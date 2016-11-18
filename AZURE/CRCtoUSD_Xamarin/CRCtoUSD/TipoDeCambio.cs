using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace CRCtoUSD
{
	public class TipoDeCambio
	{
		string id;
		decimal valorCompra;
		decimal valorVenta;
		DateTime fechaActualizacion;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value;}
		}

		[JsonProperty(PropertyName = "valorCompra")]
		public decimal ValorCompra
		{
			get { return valorCompra; }
			set { valorCompra = value;}
		}

		[JsonProperty(PropertyName = "valorVenta")]
		public decimal ValorVenta
		{
			get { return valorVenta; }
			set { valorVenta = value;}
		}

		[JsonProperty(PropertyName = "fechaActualizacion")]
		public DateTime FechaActualizacion
		{
			get { return fechaActualizacion; }
			set { fechaActualizacion = value;}
		}

        [Version]
        public string Version { get; set; }
	}
}

