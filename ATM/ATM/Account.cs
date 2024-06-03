using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public abstract class Account
    {
        int _accountNumber;
        int _password;
        int _balance = 0;

        public Account(int accountNumber, int password)
        {
            _accountNumber = accountNumber;
            _password = password;
        }

        public int AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        public int Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public virtual string Deposit(int amount)
        {
            Balance += amount;
            return $"You have deposited ${amount}. Your new balance is ${Balance}";
        }

        public virtual string Withdraw(int amount)
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

        public string Transfer(int amount, Account destinationAccount)
        {
            if (amount > Balance)
            {
                return null;
            } 
            else
            {
                Balance -= amount;
                destinationAccount.Deposit(amount);
                return $"You have transferred ${amount} to account number {destinationAccount.AccountNumber}. Your new balance is ${Balance}";
            }
            
        }

        public Account? AreYou(int InputAccountNum, int InputPassword)
        {
            if (InputAccountNum == _accountNumber && InputPassword == _password)
            {
                return this;
            }
            else
            {
                return null;
            }
        }


    }
}
