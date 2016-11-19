/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace CRCtoUSD
{
    public partial class TipoDeCambioManager
    {
        static TipoDeCambioManager defaultInstance = new TipoDeCambioManager();

        MobileServiceClient client;

        IMobileServiceTable<TipoDeCambio> tipoDeCambioDiario;


        private TipoDeCambioManager()
        {
            client = new MobileServiceClient(Constants.ApplicationURL);

            tipoDeCambioDiario = client.GetTable<TipoDeCambio>();

        }

        public static TipoDeCambioManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public async Task<TipoDeCambio> GetCurrentExchangeAsync()
        {
            try
            {
                IEnumerable<TipoDeCambio> items = await tipoDeCambioDiario
					.Where(todoItem => todoItem.FechaActualizacion == DateTime.Today)
                    .ToEnumerableAsync();

				return items.FirstOrDefault();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }
    }
}
