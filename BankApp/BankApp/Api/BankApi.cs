using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BankApp.Models;
using Refit;

namespace BankApp.Api
{
    static class BankApi
    {
        static T SafeRun<T>(Delegate method, object[] methodParams) 
        {
            try
            {
                return (T) method.DynamicInvoke(methodParams);
            }
            catch (Exception ex)
            {
                //process exception
                return default(T);
            }
        }

        static readonly IBankApi ApiClient = RestService.For<IBankApi>(AuthClass.ServerUrl);

        internal static async Task<BankUser> GetCurrentUser()
        {
            BankUser result = null;
            try
            {
                result = await BankApi.ApiClient.CurrentUser(AuthClass.Token);
            }
            catch (Exception e)
            {
                //process
            }
            return result;
        }

        internal static async Task<BankUser> CreateUser(BankUser user)
        {
            BankUser result = null;
            try
            {
                result = await BankApi.ApiClient.CreateUser(user, AuthClass.Token);
            }
            catch (Exception e)
            {
                //process
            }
            return result;
        }

        internal static async Task<BankUser> ModifyUser(BankUser user)
        {
            BankUser result = null;
            try
            {
                result = await BankApi.ApiClient.ModifyUser(user.Id, user, AuthClass.Token);
            }
            catch (Exception e)
            {
                //process
            }
            return result;
        }

        internal static async Task<CardInfo> AddCard(CardInfo cardInfo)
        {
            CardInfo result = null;
            try
            {
                result = await BankApi.ApiClient.AddCard(cardInfo, AuthClass.Token);
            }
            catch (Exception e)
            {
                //process
            }
            return result;
        }

        internal static async Task DeleteCard(CardInfo cardInfo)
        {
            try
            {
                await BankApi.ApiClient.DeleteCard(cardInfo.Id, AuthClass.Token);
            }
            catch (Exception e)
            {
                //process
            }
        }

        internal static async Task<CardInfo> ModifyCard(CardInfo cardInfo)
        {
            CardInfo result = null;
            try
            {
                result = await BankApi.ApiClient.ModifyCard(cardInfo.Id, cardInfo, AuthClass.Token);
            }
            catch (Exception e)
            {
                //process
            }
            return result;
        }

    }
}
