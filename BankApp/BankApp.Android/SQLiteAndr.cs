using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BankApp.Database;

namespace BankApp.Droid
{
    public class SQLiteAndr : ISqLite
    {
        public string GetPathToDatabase(string fileName)
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            return path;
        }
    }
}