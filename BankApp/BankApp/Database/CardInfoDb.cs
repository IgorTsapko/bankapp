using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using SQLite;

namespace BankApp.Database
{
    [Table("Cards")]
    public class CardInfoDb : SQLiteParent
    {
        public CardInfoDb()
        {
        }

        public CardInfoDb(CardInfo cardInfo, int userId)
        {
            UserId = userId;
            CardNumber = cardInfo.CardNumber;
            CardName = cardInfo.CardName;
            Balance = cardInfo.Balance;
            CVV = cardInfo.CVV;
            Month = cardInfo.Month;
            Year = cardInfo.Year;
            IsCredit = cardInfo.IsCredit;
            Id = cardInfo.Id;
        }

        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public decimal Balance { get; set; }
        public string CVV { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsCredit { get; set; }
    }
}
