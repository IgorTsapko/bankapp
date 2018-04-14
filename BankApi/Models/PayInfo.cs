using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankApi.Models
{
    public class PayInfo
    {
        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public string Description { get; set; }
    }
}