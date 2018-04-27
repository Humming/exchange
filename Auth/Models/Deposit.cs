
namespace Auth.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public Wallet Wallet { get; set; }
        public decimal Amount { get; set; }
    }
}