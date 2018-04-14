using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class CardInfo
    {
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
