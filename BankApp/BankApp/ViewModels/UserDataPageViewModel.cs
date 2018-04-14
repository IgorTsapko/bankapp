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

	    public string SaveCaption => "Сохранить";
        public string NameCaption => "Имя";
	    public string SurnameCaption => "Фамилия";
	    public string PhoneCaption => "Телефон";

        public UserDataPageViewModel(INavigationService navigationService)
        {
            SelectedUser = StateModel.CurrentUser;
            SaveCommand = new DelegateCommand(Save);
            _navigationService = navigationService;
        }

	    async void Save()
	    {
	        await BankApi.ModifyUser(new BankUser(SelectedUser));
            await StateModel.UpdateCurrentUserFromServer();
            await _navigationService.NavigateAsync(nameof(CardsInfoPage));
        }


        //{
        //    userInfo = new BankUser
        //    {
        //        Name = "Igor",
        //           Cards = new List<CardInfo>(),
        //           Pays = new List<PayInfo>(),
        //           Id = 0,
        //           PhoneNum = "0669591747",
        //           Surname = "Tsapko"
        //    };
        //    try
        //    {
        //        ///var u = await BankApi.ApiClient.CreateUser(userInfo, AuthClass.Token);
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //}
    }
}
