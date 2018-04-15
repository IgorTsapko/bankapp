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

namespace BankApp.ViewModels
{
	public class CardDetailsPageViewModel : BindableBase
	{
	    private readonly INavigationService _navigationService;
	    private readonly IEventAggregator _eventAggregator;

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

        public CardDetailsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            try
            {
                _navigationService = navigationService;
                _eventAggregator = eventAggregator;
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
            catch (Exception ex)
            {
                ExceptionProcessor.ProcessException(ex);
            }
        }

	    async void SaveCard()
	    {
	        try
	        {
	            if (AddNewCardMode)
	                await BankApi.AddCard(new CardInfo(SelectedCard));
	            else
	                await BankApi.ModifyCard(new CardInfo(SelectedCard));

	            await StateModel.UpdateCurrentUserFromServer();
	            _eventAggregator.GetEvent<UpdateCardsEvent>().Publish();

	            await _navigationService.GoBackAsync();
	        }
	        catch (Exception ex)
	        {
	            ExceptionProcessor.ProcessException(ex);
            }
	    }
	}
}
