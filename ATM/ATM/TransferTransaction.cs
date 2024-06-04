using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TransferTransaction : Transaction
    {
        public Account DestinationAccount {  get; private set; }

        public TransferTransaction(TransactionType type, int amount, Account destinationAccount)
            : base(type, amount)
        {
            this.DestinationAccount = destinationAccount;
        }

        public override string Print()
        {
            return $"{Type} of ${Amount} to {DestinationAccount.AccountNumber} on {Date}";
        }

    }
}
