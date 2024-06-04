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

        // Serialization and Deserialization Methods
        public virtual string Serialize()
        {
            return $"{Type},{Amount},{Date}";
        }

        public static Transaction Deserialize(string data)
        {
            var parts = data.Split(',');
            var type = (TransactionType)Enum.Parse(typeof(TransactionType), parts[0]);
            var amount = int.Parse(parts[1]);
            var date = DateTime.Parse(parts[2]);
            return new Transaction(type, amount) { Date = date };
        }
    }
}
