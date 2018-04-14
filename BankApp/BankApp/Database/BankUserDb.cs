using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using SQLite;

namespace BankApp.Database
{
    [Table("Users")]
    public class BankUserDb : SQLiteParent
    {
        public BankUserDb()
        {
        }

        public BankUserDb(BankUser bankUser, string eMail)
        {
            Name = bankUser.Name;
            Surname = bankUser.Surname;
            PhoneNum = bankUser.PhoneNum;
            EMail = eMail;
            LastLogin = DateTime.Now;
            Id = bankUser.Id;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNum { get; set; }
        public string EMail { get; set; }
        public bool FingerprintEnabled { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
