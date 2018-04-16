using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Api;
using BankApp.Database;
using BankApp.Models;
using BankApp.Other;
using BankApp.Views;
using Prism.Navigation;

namespace BankApp.ViewModels
{
	public class UserDataPageViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;
        public DelegateCommand SaveCommand { get; set; }

        private BankUserDb _selectedUser;
        public BankUserDb SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

	    private readonly bool _createMode;
	    public string SaveCaption => "Сохранить";
        public string NameCaption => "Имя";
	    public string SurnameCaption => "Фамилия";
	    public string PhoneCaption => "Телефон";

        public UserDataPageViewModel(INavigationService navigationService)
        {
            try
            {
                _createMode = StateModel.CurrentUser == null;
                SelectedUser = _createMode ? new BankUserDb() : StateModel.CurrentUser;

                SaveCommand = new DelegateCommand(Save);
                _navigationService = navigationService;
            }
            catch (Exception ex)
            {
                Other.ExceptionProcessor.ProcessException(ex);
            }
        }

	    async void Save()
	    {
	        try
	        {
	            if (_createMode)
	                await BankApi.CreateUser(new BankUser(SelectedUser));
	            else
	                await BankApi.ModifyUser(new BankUser(SelectedUser));
	            await StateModel.UpdateCurrentUserFromServer();
	            if (StateModel.CurrentUser != null)
	                await _navigationService.NavigateAsync(nameof(CardsInfoPage));
	        }
	        catch (Exception ex)
	        {
	            Other.ExceptionProcessor.ProcessException(ex);
	        }
        }


    }
}
