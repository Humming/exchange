using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }

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