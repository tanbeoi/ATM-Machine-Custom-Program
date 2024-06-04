using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Transaction
    {
        public TransactionType Type { get; private set; }
        public int Amount { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(TransactionType type, int amount)
        {
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
        }

        public virtual string Print()
        {
            return $"{Type} of ${Amount} on {Date}";
        }
    }
}
    