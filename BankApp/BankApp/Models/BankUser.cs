using System.Collections.Generic;
using BankApp.Database;

namespace BankApp.Models
{

    public class BankUser
    {
        public BankUser()
        {
            Cards = new List<CardInfo>();
            Pays = new List<PayInfo>();
        }

        public BankUser(BankUserDb bankUserDb)
        {
            Id = bankUserDb.Id;
            Name = bankUserDb.Name;
            Surname = bankUserDb.Surname;
            PhoneNum = bankUserDb.PhoneNum;
            Cards = new List<CardInfo>();
            Pays = new List<PayInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNum { get; set; }
        public List<CardInfo> Cards { get; set; }
        public List<PayInfo> Pays { get; set; }
    }
}
