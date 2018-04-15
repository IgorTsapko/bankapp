using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Api;
using BankApp.Database;
using BankApp.Other;

namespace BankApp.ViewModels
{
	public class BankBranchesPageViewModel : BindableBase
	{
	    public string PageTitle => "Список отделений";

        private List<BankBranchDb> _branches;

	    public List<BankBranchDb> Branches
	    {
	        get => _branches;
	        set => SetProperty(ref _branches, value);
	    }

	    async void ReadList()
	    {
	        var branches = await BankApi.GetBranches();
	        Branches = new List<BankBranchDb>();

            if (branches != null && branches.Count > 0)
            {
                Branches = branches.Select(branche => new BankBranchDb(branche)).ToList();
                foreach (var branche in Branches)
                    Repository.SaveItem(branche);
            }
	        else
            {
                Branches = Repository.GetItems<BankBranchDb>().ToList();
            }
	    }

	    public BankBranchesPageViewModel()
	    {
	        try
	        {
	            ReadList();
            }
	        catch (Exception ex)
	        {
	            Other.ExceptionProcessor.ProcessException(ex);
            }
	        
	    }
	}
}
