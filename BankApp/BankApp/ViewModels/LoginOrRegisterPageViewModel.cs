using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Api;
using BankApp.Database;
using BankApp.EventTypes;
using BankApp.Models;
using BankApp.Other;
using BankApp.Views;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
	public class LoginOrRegisterPageViewModel : BindableBase
	{
	    public Keyboard EMailKeyboard = Keyboard.Email;


        private readonly IEventAggregator _eventAggregator;
	    private readonly INavigationService _navigationService;


        private bool _registrationMode;
        public bool RegistrationMode
        {
            get => _registrationMode;
            set
            {
                SetProperty(ref _registrationMode, value); 
                RaisePropertyChanged(nameof(LoginButtonCaption));
                RaisePropertyChanged(nameof(RegisterButtonCaption));
                RaisePropertyChanged(nameof(LoginMode));
                RaisePropertyChanged(nameof(PageTitle));
            }
        }

	    public bool LoginMode => !RegistrationMode;


        public string PageTitle => RegistrationMode ? "Регистрация" : "Вход";
        public string LoginButtonCaption => RegistrationMode ? "Регистрация" : "Вход";
	    public string RegisterButtonCaption => RegistrationMode ? "Отмена" : "Зарегистрироваться";
        public string EMailCaption => "Ваш E-mail";
	    public string PasswordCaption => "Пароль";
	    public string PasswordConfirmCaption => "Подтверждение пароля";



	    private string _eMailValue = "user@gmail.com";
	    public string EMailValue
        {
	        get => _eMailValue;
	        set => SetProperty(ref _eMailValue, value);
	    }

	    private string _passwordValue = "qwerty";
	    public string PasswordValue
        {
	        get => _passwordValue;
	        set => SetProperty(ref _passwordValue, value);
	    }

	    private string _passwordConfirmValue;
	    public string PasswordConfirmValue
        {
	        get => _passwordConfirmValue;
	        set => SetProperty(ref _passwordConfirmValue, value);
	    }



        public DelegateCommand LoginOrRegisterCommand { get; }
	    public DelegateCommand OpenRegisterSectionCommand { get; }

        public LoginOrRegisterPageViewModel(IEventAggregator eventAggregator, INavigationService navigationService, ISqLite sqLiteImpl)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            Repository.DatabasePath = sqLiteImpl.GetPathToDatabase("bankApp.db");
            Repository.Init();
            StateModel.UpdateCurrentUserFromCache();
            if (StateModel.CurrentUser != null)
                EMailValue = StateModel.CurrentUser.EMail;

            LoginOrRegisterCommand = new DelegateCommand(LoginOrRegister);
            OpenRegisterSectionCommand = new DelegateCommand(OpenRegister);
        }

	    async void LoginOrRegister()
	    {
	        bool result = RegistrationMode ? 
                AuthClass.Register(EMailValue, PasswordValue, PasswordConfirmValue) 
                : AuthClass.Auth(EMailValue, PasswordValue);

	        if (!result)
	            _eventAggregator.GetEvent<ShowAlertEvent>().Publish("Произошла ошибка");
	        else
	        {
                await StateModel.UpdateCurrentUserFromServer(EMailValue);

	            if (StateModel.CurrentUser == null)
	            {
	                await _navigationService.NavigateAsync(nameof(UserDataPage));
	            }
	            else
	            {
                    await _navigationService.NavigateAsync(nameof(CardsInfoPage));
	            }
	        }

        }

	    void OpenRegister()
	    {
            RegistrationMode = !RegistrationMode;
        }
	}
}
