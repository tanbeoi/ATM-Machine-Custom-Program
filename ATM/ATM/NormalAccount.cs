using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class NormalAccount : Account
    {
        public NormalAccount(int accountNumber, int password)
            : base(accountNumber, password)
        {
        }

        public override string Withdraw(int amount)
        {
            if (amount > Balance)
            {
                return null;
            }
            else
            {
                Balance -= amount;
                return $"You have withdrawn ${amount}. Your new balance is ${Balance}";
            }
        }

        public override string Deposit(int amount)
        {
            Balance += amount;
            return $"You have deposited ${amount}. Your new balance is ${Balance}";
        }
    }
}
