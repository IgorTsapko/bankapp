using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using SQLite;

namespace BankApp.Database
{
    [Table("Pays")]
    public class PayInfoDb : SQLiteParent
    {
        public PayInfoDb()
        {
        }

        public PayInfoDb(PayInfo payInfo, int userId)
        {
            UserId = userId;
            CardId = payInfo.CardId;
            Sum = payInfo.Sum;
            Description = payInfo.Description;
            Id = payInfo.Id;
        }

        public int UserId { get; set; }
        public int CardId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }
    }
}
