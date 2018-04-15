using BankApp.Api;
using BankApp.EventTypes;
using Prism.Events;
using Xamarin.Forms;

namespace BankApp.Views
{
    public partial class LoginOrRegisterPage : ContentPage
    {
        private readonly IEventAggregator _eventAggregator;
        public LoginOrRegisterPage(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AuthClass.Logoff();
            _eventAggregator.GetEvent<ShowAlertEvent>().Subscribe(ShowAlert);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _eventAggregator.GetEvent<ShowAlertEvent>().Unsubscribe(ShowAlert);
        }

        async void ShowAlert(string text)
        {
            await DisplayAlert("", text, "Ok");
        }
    }
}
