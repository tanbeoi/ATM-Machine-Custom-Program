using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TransferTransaction : Transaction
    {
        public int DestinationAccountNumber { get; private set; }

        public TransferTransaction(TransactionType type, int amount, Account destinationAccount)
            : base(type, amount)
        {
            this.DestinationAccountNumber = destinationAccount.AccountNumber;
        }

        public override string Print()
        {
            return $"{Type} of ${Amount} to {DestinationAccountNumber} on {Date}";
        }

        // Serialization and Deserialization Methods
        public override string Serialize()
        {
            return $"{base.Serialize()},{DestinationAccountNumber}";
        }

        public static TransferTransaction Deserialize(string data, List<Bank> banks)
        {
            var parts = data.Split(',');
            var baseTransaction = Transaction.Deserialize(string.Join(",", parts.Take(3).ToArray()));
            var destinationAccountNumber = int.Parse(parts[3]);

            // Find the destination account
            Account destinationAccount = null;
            foreach (var bank in banks)
            {
                destinationAccount = bank.GetTransferAccount(destinationAccountNumber);
                if (destinationAccount != null)
                {
                    break;
                }
            }

            if (destinationAccount == null)
            {
                throw new Exception("Destination account not found");
            }

            return new TransferTransaction(baseTransaction.Type, baseTransaction.Amount, destinationAccount) { Date = baseTransaction.Date };
        }

    }
}
