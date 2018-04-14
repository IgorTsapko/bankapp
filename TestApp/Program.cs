﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace TestApp
{
    class Program
    {
        private static string EMailValue = "user@gmail.com";
        private static string PasswordValue = "qwerty";
        static void Main(string[] args)
        {
            //var _connection = new SQLiteConnection("ttt");

            //Type ourtype = typeof(SQLiteParent);
            //IEnumerable<Type> list = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype));
            ////MethodInfo method = typeof(SQLiteConnection).GetMethod("CreateTable",
            ////    BindingFlags.Public | BindingFlags.CreateInstance);
            //foreach (var l in list)
            //{
            //    MethodInfo method = typeof(SQLiteConnection).GetMethod("CreateTable").MakeGenericMethod(new Type[] { l });
            //}
            


            //return;
            while (true)
            {
                DoIt();
                Console.WriteLine("a");
                Console.ReadKey();
            }
        }

        static async void DoIt()
        {
            bool RegistrationMode = false;
            bool result =
                RegistrationMode ?
                AuthClass.Register(EMailValue, PasswordValue, PasswordValue)
                : 
                AuthClass.Auth(EMailValue, PasswordValue);

            if (result)
            {
                var SelectedCard = new CardInfo
                {
                    CardName = "test card 1",
                    Balance = 5,
                    CardNumber = "5168742711605224",
                    CVV = "123",
                    IsCredit = true,
                    Month = 11,
                    Year = 2019
                };
                await BankApi.ApiClient.ModifyCard(1, SelectedCard, AuthClass.Token);
                await BankApi.ApiClient.DeleteCard(1, AuthClass.Token);

                //var br = await BankApi.ApiClient.GetBranches();
                //var userInfo = await BankApi.GetCurrentUser();
                //if (userInfo == null)
                //{
                //    userInfo = new BankUser
                //    {
                //        Name = "Igor",
                //        Cards = new List<CardInfo>(),
                //        Pays = new List<PayInfo>(),
                //        Id = 0,
                //        PhoneNum = "0669591747",
                //        Surname = "Tsapko"
                //    };
                //    try
                //    {
                //        var u = await BankApi.ApiClient.CreateUser(userInfo, AuthClass.Token);
                //    }
                //    catch (Exception e)
                //    {
                //    }
                //}
                //else
                //{
                //    try
                //    {
                //        userInfo.Name = "Igor2";
                //        var u = await BankApi.ApiClient.ModifyUser(userInfo.Id, userInfo, AuthClass.Token);
                //    }
                //    catch (Exception e)
                //    {
                //    }
                //}
            }
        }
    }

    static class BankApi
    {
        internal static IBankApi ApiClient = RestService.For<IBankApi>(AuthClass.ServerUrl);

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

    }
}
