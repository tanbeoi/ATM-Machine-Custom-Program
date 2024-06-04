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
            double extra = amount * (InterestRate / 100.0);
            Console.WriteLine(extra);
            int total = amount + (int)extra;
            base.Deposit(total);
            Transaction transaction = new Transaction(TransactionType.Deposit, amount);
            AddTransaction(transaction);
            return ($"Interest applied at {InterestRate}% for deposit of ${amount}. Now your balance is ${Balance}");
        }

    }
}
