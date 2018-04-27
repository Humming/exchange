using System;

namespace Auth.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        public Guid WalletIdentifier { get; set; } = Guid.NewGuid();

        public decimal Balance { get; set; } = 100;

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }


        public void Deposit(Deposit deposit)
        {

        }

        public bool IsValid()
        {
            return Balance > 0;
        }

        public void WithDrawal()
        {
            throw new NotImplementedException("All your money is mine :D"); 
        }

    }
}