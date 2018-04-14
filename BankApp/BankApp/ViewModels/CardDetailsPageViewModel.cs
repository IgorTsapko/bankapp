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
	public class CardDetailsPageViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;

        private CardInfoDb _selectedCard;
        public CardInfoDb SelectedCard
	    {
	        get => _selectedCard;
	        set => SetProperty(ref _selectedCard, value);

	    }

	    public string PageTitle => AddNewCardMode ? "Добавление" : SelectedCard.CardName;

	    public string NameCaption => "Название";
	    public string NumberCaption => "Номер";
	    public string BalanceCaption => "Остаток";
	    public string CVVCaption => "CVV-код";
	    public string MonthCaption => "Месяц";
	    public string YearCaption => "Год";
	    public string CreditCaption => "Кредитная";
	    public string SaveCaption => "Сохранить";


        public bool AddNewCardMode { get;  }

        public DelegateCommand SaveCommand { get; set; }

        public CardDetailsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddNewCardMode = StateModel.SelectedCard == null;
            if (AddNewCardMode)
            {
                SelectedCard = new CardInfoDb
                {
                    CardName = "test card 1",
                    Balance = 5,
                    CardNumber = "5168742711605224",
                    CVV = "123",
                    IsCredit = true,
                    Month = 11,
                    Year = 2019
                };

            }
            else
                SelectedCard = StateModel.SelectedCard;

            SaveCommand = new DelegateCommand(SaveCard);
        }

	    async void SaveCard()
	    {
	        if (AddNewCardMode)
	            await BankApi.AddCard(new CardInfo(SelectedCard));
	        else
	            await BankApi.ModifyCard(new CardInfo(SelectedCard));

	        await StateModel.UpdateCurrentUserFromServer();
	        await _navigationService.GoBackAsync();
        }
	}
}
