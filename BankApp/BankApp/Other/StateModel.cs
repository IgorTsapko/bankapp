using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Api;
using BankApp.Database;
using BankApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Other
{
    class StateModel
    {
        internal static CardInfoDb SelectedCard { get; set; }

        internal static BankUserDb CurrentUser { get; private set; }

        internal static async Task UpdateCurrentUserFromServer(string eMail = "")
        {
            try
            {
                if (String.IsNullOrEmpty(eMail) && CurrentUser?.EMail != null)
                    eMail = CurrentUser.EMail;

                var user = await BankApi.GetCurrentUser();
                user.Cards = new List<CardInfo>(await BankApi.GetCards());
                user.Pays = new List<PayInfo>(await BankApi.GetPays());

                CurrentUser = new BankUserDb(user, eMail);
                var allCards = Repository.GetItems<CardInfoDb>().Where(c => c.UserId == user.Id).ToList();
                foreach (var cardInfo in allCards)
                    Repository.DeleteItem<CardInfoDb>(cardInfo.Id);

                var allPays = Repository.GetItems<PayInfoDb>().Where(c => c.UserId == user.Id).ToList();
                foreach (var payInfo in allPays)
                    Repository.DeleteItem<CardInfoDb>(payInfo.Id);
                if (user.Cards != null)
                    foreach (var userCard in user.Cards)
                        Repository.SaveItem<CardInfoDb>(new CardInfoDb(userCard, user.Id));

                if (user.Pays != null)
                    foreach (var userPay in user.Pays)
                        Repository.SaveItem<PayInfoDb>(new PayInfoDb(userPay, user.Id));

                Repository.SaveItem(CurrentUser);
            }
            catch (Exception ex)
            {
                CurrentUser = null;
                //
            }
        }

        internal static void ReadLastLogin()
        {
            try
            {
                CurrentUser = Repository.GetItems<BankUserDb>().OrderByDescending(u => u.LastLogin).First();
            }
            catch (Exception ex)
            {
                //
            }
        }
    }
}
