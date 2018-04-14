using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class BankBranch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
