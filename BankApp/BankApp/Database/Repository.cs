using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BankApp.Models;
using SQLite;

namespace BankApp.Database
{
    static class Repository
    {
        internal static string DatabasePath;
        static SQLiteConnection _connection;

        internal static void Init()
        {
            _connection = new SQLiteConnection(DatabasePath);

            try
            {

                _connection.CreateTable<BankUserDb>();
                _connection.CreateTable<CardInfoDb>();
                _connection.CreateTable<PayInfoDb>();
            }
            catch (Exception ex)
            {
                //process
            }

            //Type ourtype = typeof(SQLiteParent); 
            //IEnumerable<Type> list = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype));

            //foreach (var myType in list)
            //{
            //    var typeOfConnection = _connection.GetType();
            //    var method = typeOfConnection.GetMethod("CreateTable", BindingFlags.Instance | BindingFlags.Public);
            //    method = method.MakeGenericMethod(myType);
            //    method.Invoke(_connection, new object[] { });
            //}
        }

        public static IEnumerable<T> GetItems<T>() where T : new()
        {
            return (from i in _connection.Table<T>() select i).ToList();
        }
        public static T GetItem<T>(int id) where T : new()
        {
            return _connection.Get<T>(id);
        }
        public static int DeleteItem<T>(int id) where T : new()
        {
            return _connection.Delete<T>(id);
        }
        public static void SaveItem<T>(T item) where T : SQLiteParent, new()
        {
            if (_connection.Update(item) == 0)
                _connection.Insert(item);
        }
    }
}
