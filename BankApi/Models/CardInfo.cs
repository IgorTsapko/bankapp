using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BankApi.Models
{
    public class CardInfo : IValidatableObject
    {
        public int Id { get; set; }

        [CreditCard]
        [Required]
        public string CardNumber { get; set; }

        [MinLength(5)]
        [Required]
        public string CardName { get; set; }

        public decimal Balance { get; set; } = 0;

        [MinLength(3)]
        [MaxLength(3)]
        [Required]
        public string CVV { get;set; }

        [Range(1, 12)]
        [Required]
        public int Month { get; set; }

        [Range(2010, 2030)]
        [Required]
        public int Year { get; set; }

        public bool IsCredit { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (IsCredit && Balance < 0)
                results.Add(new ValidationResult("Card is not credit, so balance can't be less than zero"));

            return results;
        }
    }
}