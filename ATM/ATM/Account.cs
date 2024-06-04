using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ATM
{
    public abstract class Account
    {
        List<Transaction> _transactions = new List<Transaction>();
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

        public List<Transaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; }
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public virtual string Deposit(int amount)
        {
            Balance += amount;
            Transaction transaction = new Transaction(TransactionType.Deposit, amount);
            AddTransaction(transaction);
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
                Transaction transaction = new Transaction(TransactionType.Withdraw, amount);
                AddTransaction(transaction);
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
                TransferTransaction transaction = new TransferTransaction(TransactionType.Transfer, amount, destinationAccount);
                AddTransaction(transaction);
                return $"You have transferred ${amount} to account number {destinationAccount.AccountNumber}. Your new balance is ${Balance}";
            }
            
        }


        public string TransactionHistory()
        {
            int order = 1;
            string history = "";
            foreach (Transaction transaction in _transactions)
            {
                history += order + ". " + transaction.Print() + "\n";
                order++;
            }
            return history;
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

        // Serialization and Deserialization Methods
        public string Serialize()
        {
            var transactionsString = string.Join(";", _transactions.Select(t => t.Serialize()));
            return $"{GetType().Name},{_accountNumber},{_password},{_balance},{transactionsString}";
        }

        public static Account Deserialize(string data)
        {
            var parts = data.Split(',');
            var accountType = parts[0];
            var accountNumber = int.Parse(parts[1]);
            var password = int.Parse(parts[2]);
            var balance = int.Parse(parts[3]);
            var transactionsString = parts.Length > 4 ? parts[4] : string.Empty;

            Account account;
            if (accountType == nameof(NormalAccount))
            {
                account = new NormalAccount(accountNumber, password);
            }
            else if (accountType == nameof(SavingsAccount))
            {
                var interestRate = int.Parse(parts[4]);
                transactionsString = parts.Length > 5 ? parts[5] : string.Empty;
                account = new SavingsAccount(accountNumber, password, interestRate);
            }
            else if (accountType == nameof(CheckingAccount))
            {
                var overdraftLimit = int.Parse(parts[4]);
                transactionsString = parts.Length > 5 ? parts[5] : string.Empty;
                account = new CheckingAccount(accountNumber, password, overdraftLimit);
            }
            else
            {
                throw new Exception("Unknown account type.");
            }

            account.Balance = balance;

            if (!string.IsNullOrEmpty(transactionsString))
            {
                var transactions = transactionsString.Split(';');
                foreach (var transactionString in transactions)
                {
                    var transaction = Transaction.Deserialize(transactionString);
                    account.AddTransaction(transaction);
                }
            }

            return account;
        }


    }
}
