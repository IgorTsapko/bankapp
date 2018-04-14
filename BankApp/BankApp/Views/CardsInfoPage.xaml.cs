using BankApp.Api;
using BankApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace BankApp.Views
{
    public partial class CardsInfoPage : ContentPage
    {
        public CardsInfoPage()
        {
            InitializeComponent();
           
            ToolbarItems.Clear();
            ToolbarItems.Add(new ToolbarItem{
                Text = "logoff",
                Icon = "logout.png",
                Command = new DelegateCommand(Logoff)
            });

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "user",
                Icon = "user.png",
                Command = new DelegateCommand(ModifyUser)
            });
        }

        void ModifyUser()
        {
            ((CardsInfoPageViewModel)BindingContext).ModifyUser();
        }

        void Logoff()
        {
            AuthClass.Logoff();
            ((CardsInfoPageViewModel)BindingContext).Logoff();
           
        }
    }
}
