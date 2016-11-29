using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRCtoUSD
{
    public partial class TipoDeCambioForm : ContentPage
    {
        TipoDeCambioManager manager;

        public TipoDeCambioForm()
        {
            InitializeComponent();

            manager = TipoDeCambioManager.DefaultManager;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            await RefreshItems(true);
        }

        private async Task RefreshItems(bool showActivityIndicator)
        {
            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
				var todaysTipoDeCambio = await manager.GetCurrentExchangeAsync();
				valorCompra.Text = todaysTipoDeCambio.ValorCompra.ToString();
				valorVenta.Text = todaysTipoDeCambio.ValorVenta.ToString();
            }
        }

		public void OnCalculateButtonClicked(object sender, EventArgs args)
		{
			Button button = (Button)sender;
			if (button.Text == "Get CRC")
			{
				decimal usdAmount;
				var isUsdAmount = decimal.TryParse(usdEntry.Text, out usdAmount);

				if (isUsdAmount)
				{
					crcEntry.Text = String.Format("{0:0.##}",(usdAmount * decimal.Parse(valorVenta.Text)));
				}
			}
			else {
				decimal crcAmount;
				var isCrcAmount = decimal.TryParse(crcEntry.Text, out crcAmount);

				if (isCrcAmount)
				{
					usdEntry.Text = String.Format("{0:0.##}",(crcAmount / decimal.Parse(valorVenta.Text)));
				}
			}
		}

        private class ActivityIndicatorScope : IDisposable
        {
            private bool showIndicator;
            private ActivityIndicator indicator;
            private Task indicatorDelay;

            public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
            {
                this.indicator = indicator;
                this.showIndicator = showIndicator;

                if (showIndicator)
                {
                    indicatorDelay = Task.Delay(2000);
                    SetIndicatorActivity(true);
                }
                else
                {
                    indicatorDelay = Task.FromResult(0);
                }
            }

            private void SetIndicatorActivity(bool isActive)
            {
                this.indicator.IsVisible = isActive;
                this.indicator.IsRunning = isActive;
            }

            public void Dispose()
            {
                if (showIndicator)
                {
                    indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
                }
            }

        }
    }
}

