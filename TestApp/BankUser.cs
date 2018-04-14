using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class SQLiteParent
    {
    }

    public class BankUser : SQLiteParent
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public string PhoneNum { get; set; }

        public List<CardInfo> Cards { get; set; }
        public List<PayInfo> Pays { get; set; }

        //public bool FingerprintEnabled { get; set; }

    }
}
