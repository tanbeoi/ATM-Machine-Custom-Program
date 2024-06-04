using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class NormalAccount : Account
    {
        public NormalAccount(int accountNumber, int password)
            : base(accountNumber, password)
        {
        }

    }
}
