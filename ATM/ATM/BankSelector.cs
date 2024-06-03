using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class BankSelector
    {
        internal List<Bank> _banks = new List<Bank>();
        public BankSelector(List<Bank> banks) 
        { 
            _banks = banks;
            foreach (Bank bank in _banks)
            {
                int num = _banks.IndexOf(bank) + 1;
                bank.AddId(num.ToString());
            }
        }

        public Bank? SelectBank()
        {
            Console.WriteLine("Please select a bank: \n" + BankList + "\nYour Option:");
            string InputBank = Console.ReadLine().ToLower().Trim();

            foreach (Bank bank in _banks)
            {
                if (bank.AreYou(InputBank))
                {
                    return bank;
                }
            }
            return null;
        }

        public string BankList
        {
            get
            {
                int i = 1;
                string bankList = "";
                foreach (Bank bank in _banks)
                {
                    bankList += i + ". " + bank.Name + " (" + bank.Id + ")\n";
                    i++;
                }
                return bankList;
            }
        }
    }
}
