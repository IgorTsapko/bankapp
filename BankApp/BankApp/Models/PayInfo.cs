namespace BankApp.Models
{
    public class PayInfo
    {
        public int Id { get; set; }
        public int CardId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }
    }
}
