using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Database;
using BankApp.Other;

namespace BankApp.ViewModels
{
	public class PaysInfoPageViewModel : BindableBase
	{
	    public string PageTitle { get; }

	    private List<PayInfoDb> _pays;
        public List<PayInfoDb> Pays
        {
            get => _pays;
            set => SetProperty(ref _pays, value);
        }

        public PaysInfoPageViewModel()
        {
            try
            {
                PageTitle = StateModel.SelectedCard.CardName;
                Pays = Repository.GetItems<PayInfoDb>().Where(p => p.CardId == StateModel.SelectedCard.Id).ToList();
            }
            catch (Exception ex)
            {
                Other.ExceptionProcessor.ProcessException(ex);
            }
        }
	}
}
