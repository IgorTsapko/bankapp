using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BankApi.Models;

namespace BankApi.DbCont
{
    public class BankContext : DbContext
    {
        public BankContext() : base("BankDataDbConnection")
        {
        }

        public DbSet<BankUser> BankUsers { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
    }
}