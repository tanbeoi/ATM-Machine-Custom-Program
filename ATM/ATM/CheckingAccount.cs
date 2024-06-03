using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class CheckingAccount : Account
    {
        public int OverdraftLimit { get; private set; }

        public CheckingAccount(int accountNumber, int password, int overdraftLimit)
            : base(accountNumber, password)
        {
            OverdraftLimit = overdraftLimit;
        }


        public override string Withdraw(int amount)
        {
            if (amount <= Balance)
            {
                base.Withdraw(amount);
                return $"You have withdrawn ${amount}. Your new balance is ${Balance}";
            } 
            else if (amount <= Balance + OverdraftLimit)
            {
                int overdraft = amount - Balance;
                OverdraftLimit -= overdraft;
                base.Withdraw(Balance);
                return $"You have withdrawn ${amount}. Your new balance is ${Balance}. You have used ${overdraft} of your overdraft limit. Your Overdraft limit is ${OverdraftLimit} left.";
            }
            else
            {
                return null;
            }
        }

        public override string Deposit(int amount)
        {
            base.Deposit(amount);
            return $"You have deposited ${amount}. Your new balance is ${Balance}";
        }


    }
}
