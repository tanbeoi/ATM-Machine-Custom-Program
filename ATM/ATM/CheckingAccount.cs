﻿using System;
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
                Transaction transaction = new Transaction(TransactionType.Withdraw, amount);
                AddTransaction(transaction);
                return $"You have withdrawn ${amount}. Your new balance is ${Balance}";
            } 
            else if (amount <= Balance + OverdraftLimit)
            {
                int overdraft = amount - Balance;
                OverdraftLimit -= overdraft;
                base.Withdraw(Balance);
                Transaction transaction = new Transaction(TransactionType.Withdraw, amount);
                AddTransaction(transaction);
                return $"You have withdrawn ${amount}. Your new balance is ${Balance}. You have used ${overdraft} of your overdraft limit. Your Overdraft limit is ${OverdraftLimit} left.";
            }
            else
            {
                return null;
            }
        }


    }
}
