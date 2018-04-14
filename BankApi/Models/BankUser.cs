using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankApi.Models
{
    public class BankUser
    {
        public BankUser()
        {
            Cards = new List<CardInfo>();
            Pays = new List<PayInfo>();
        }

        public int Id { get; set; }

        //[Index(IsUnique = true)]
        public string UserIdentityId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [Phone]
        public string PhoneNum { get; set; }
        
        public List<CardInfo> Cards { get; set; }
        public List<PayInfo> Pays { get; set; }
    }
}