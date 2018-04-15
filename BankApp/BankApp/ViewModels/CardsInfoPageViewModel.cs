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
	public class CardsInfoPageViewModel : BindableBase
	{
	    public DelegateCommand AddCardCommand { get; set; }
	    public DelegateCommand<CardInfoDb> ModifyCardCommand { get; set; }
	    public DelegateCommand<CardInfoDb> DeleteCardCommand { get; set; }
	    public DelegateCommand<CardInfoDb> OpenCardCommand { get; set; }

        private List<CardInfoDb> _userCards;

        public List<CardInfoDb> UserCards
        {
            get => _userCards;
            set
            {
                SetProperty(ref _userCards, value); 
                RaisePropertyChanged(nameof(CardInfoDb.CardName));
                RaisePropertyChanged(nameof(CardInfoDb.CardNumber));
                RaisePropertyChanged(nameof(CardInfoDb.Balance));
            }
        }

	    private readonly INavigationService _navigationService;
	    private readonly IEventAggregator _eventAggregator;


        public string AddCartButtonCaption => "Добавить карту";
	    public string DeleteCartButtonCaption => "Удалить";
	    public string ModifyCartButtonCaption => "Изменить";
	    public string OpenCartButtonCaption => "Детали";
        public string PageTitle => StateModel.CurrentUser.Name;
        public CardsInfoPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            try
            {
                _navigationService = navigationService;
                _eventAggregator = eventAggregator;
                _eventAggregator.GetEvent<UpdateCardsEvent>().Subscribe(UpdateCards);
                UpdateCards();
                OpenCardCommand = new DelegateCommand<CardInfoDb>(OpenCart);
                DeleteCardCommand = new DelegateCommand<CardInfoDb>(DeleteCart);
                ModifyCardCommand = new DelegateCommand<CardInfoDb>(ModifyCart);
                AddCardCommand = new DelegateCommand(AddCart);
            }
            catch (Exception ex)
            {
                Other.ExceptionProcessor.ProcessException(ex);
            }
        }

	    void UpdateCards()
	    {
	        try
	        {
	            if (StateModel.CurrentUser != null)
	                UserCards = Repository.GetItems<CardInfoDb>().Where(c => c.UserId == StateModel.CurrentUser.Id)
	                    .ToList();
	        }
	        catch (Exception ex)
	        {
	            Other.ExceptionProcessor.ProcessException(ex);
            }
	    }

	    internal async void AddCart()
	    {
	        try
	        {
	            StateModel.SelectedCard = null;
	            await _navigationService.NavigateAsync(nameof(CardDetailsPage));
	        }
	        catch (Exception ex)
	        {
	            Other.ExceptionProcessor.ProcessException(ex);
            }
	    }
        
	    internal async void ModifyCart(CardInfoDb cardInfo)
	    {
	        try
	        {
	            StateModel.SelectedCard = cardInfo;
	            await _navigationService.NavigateAsync(nameof(CardDetailsPage));
	        }
	        catch (Exception ex)
	        {
	            Other.ExceptionProcessor.ProcessException(ex);
            }

	    }

	    internal async void DeleteCart(CardInfoDb cardInfo)
	    {
	        try
	        {
	            Repository.DeleteItem<CardInfoDb>(cardInfo.Id);
	            await BankApi.DeleteCard(new CardInfo(cardInfo));
	            await StateModel.UpdateCurrentUserFromServer();
	            UpdateCards();

	        }
	        catch (Exception ex)
	        {
	            Other.ExceptionProcessor.ProcessException(ex);
            }
	    }


        internal async void OpenCart(CardInfoDb cardInfo)
	    {
	        StateModel.SelectedCard = cardInfo;
            await _navigationService.NavigateAsync(nameof(PaysInfoPage));
        }

	    internal async void ModifyUser()
	    {
            await _navigationService.NavigateAsync(nameof(UserDataPage));
        }

	    internal async void Logoff()
	    {
            await _navigationService.NavigateAsync(nameof(LoginOrRegisterPage));
	    }
    }
}
