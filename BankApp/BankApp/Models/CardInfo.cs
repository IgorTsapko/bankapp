using BankApp.Database;

namespace BankApp.Models
{
    public class CardInfo
    {
        public CardInfo()
        { }

        public CardInfo(CardInfoDb cardInfo)
        {
            CardNumber = cardInfo.CardNumber;
            CardName = cardInfo.CardName;
            Balance = cardInfo.Balance;
            CVV = cardInfo.CVV;
            Month = cardInfo.Month;
            Year = cardInfo.Year;
            IsCredit = cardInfo.IsCredit;
            Id = cardInfo.Id;
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public decimal Balance { get; set; } 
        public string CVV { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsCredit { get; set; } 

    }
}
