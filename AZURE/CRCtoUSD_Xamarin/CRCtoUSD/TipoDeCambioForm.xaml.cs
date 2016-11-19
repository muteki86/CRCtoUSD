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

        //async Task CompleteItem(TipoDeCambio item)
        //{
            //item. = true;
            //await manager.SaveTaskAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
        //}

        //public async void OnAdd(object sender, EventArgs e)
        //{
            //var todo = new TipoDeCambio { ValorCompra = newItemName.Text };
            //await AddItem(todo);

            //newItemName.Text = string.Empty;
            //newItemName.Unfocus();
        //}

        // Event handlers
		/*
        public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todo = e.SelectedItem as TipoDeCambio;
            if (Device.OS != TargetPlatform.iOS && todo != null)
            {
                // Not iOS - the swipe-to-delete is discoverable there
                if (Device.OS == TargetPlatform.Android)
                {
                    //await DisplayAlert(todo.ValorCompra, "Press-and-hold to complete task " + todo.ValorCompra, "Got it!");
                }
                else
                {
                    // Windows, not all platforms support the Context Actions yet
                    if (await DisplayAlert("Mark completed?", "Do you wish to complete " + todo.ValorCompra + "?", "Complete", "Cancel"))
                    {
                        await CompleteItem(todo);
                    }
                }
            }

            // prevents background getting highlighted
            todoList.SelectedItem = null;
        }
        */

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#context
        /*
		public async void OnComplete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var todo = mi.CommandParameter as TipoDeCambio;
            await CompleteItem(todo);
        }
        */

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#pulltorefresh
        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }

        public async void OnSyncItems(object sender, EventArgs e)
        {
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

