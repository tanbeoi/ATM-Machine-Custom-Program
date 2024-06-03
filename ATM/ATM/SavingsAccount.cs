using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class SavingsAccount : Account
    {
        private int _interestRate;
        public int InterestRate
        {
            get { return _interestRate; }
            private set
            {
                if (value == 3 || value == 5)
                {
                    _interestRate = value;
                }
                else
                {
                    throw new ArgumentException("Interest rate must be either 3 or 5.");
                }
            }
        }

        public SavingsAccount(int accountNumber, int password, int interestRate)
            : base(accountNumber, password)
        {
            InterestRate = interestRate;
        }

        public override string Deposit(int amount)
        {
            int extra = (amount * InterestRate) / 100;
            base.Deposit(amount + extra);
            
            return ($"Interest applied at {InterestRate}% for deposit of ${amount}. Now your balance is ${Balance}");
        }

        public override string Withdraw(int amount)
        {
            if (amount <= Balance)
            {
                base.Withdraw(amount);
                return $"You have withdrawn ${amount}. Your new balance is ${Balance}";
            }
            else
            {
                return null;
            }
        }
    }
}
