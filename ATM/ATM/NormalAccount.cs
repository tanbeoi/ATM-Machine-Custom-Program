using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

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
                Transaction transaction = new Transaction(TransactionType.Withdraw, amount);
                AddTransaction(transaction);
                return $"You have withdrawn ${amount}. Your new balance is ${Balance}";
            }
        }

        public override string Deposit(int amount)
        {
            Balance += amount;
            Transaction transaction = new Transaction(TransactionType.Deposit, amount);
            AddTransaction(transaction);
            return $"You have deposited ${amount}. Your new balance is ${Balance}";
        }

        // Serialization and Deserialization Methods
        public static string Serialize(List<Account> accounts)
        {
            return JsonSerializer.Serialize(accounts);
        }

        public static List<Account> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<List<Account>>(json);
        }
    }
}
