using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using SQLite;

namespace BankApp.Database
{
    [Table("Users")]
    public class BankBranchDb : SQLiteParent
    {
        public BankBranchDb()
        {
        }

        public BankBranchDb(BankBranch bankBranch)
        {
            Name = bankBranch.Name;
            Number = bankBranch.Number;
            Latitude = bankBranch.Latitude;
            Longitude = bankBranch.Longitude;
            Id = bankBranch.Id;
        }
        public string Name { get; set; }
        public int Number { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
