using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Bank
    {
        private List<Account> _accounts = new List<Account>();
        private List<string> _ids = new List<string>();
        private string _name;
        public Bank(List<string> ids, string name)
        {
            foreach (string id in ids)
            {
                _ids.Add(id.ToLower());
                _name = name;
            }
        }

        public string Id
        {
            get { return _ids[0].ToUpper(); }
        }

        public void AddId(string id)
        {
            _ids.Add(id.ToLower());
        }

        public bool AreYou(string id)
        {
            if (_ids.Contains(id.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Name
        {
            get { return _name; }
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public void DeleteAccount(Account account)
        {
            _accounts.Remove(account);
        }

        public Account? GetAccount(int accountNumber, int password)
        {
            foreach (Account account in _accounts)
            {
                if (account.AccountNumber == accountNumber && account.Password == password)
                {
                    return account;
                }
            }
            return null;
        }

        public Account? GetTransferAccount(int accountNumber)
        {
            foreach (Account account in _accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
    }
}
