using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Set up banks and accounts
            Bank cba = new Bank(new List<string> {"CBA"}, "Commonwealth");
            Account account1 = new NormalAccount(123456, 1234);
            SavingsAccount account3 = new SavingsAccount(123457, 1234, 3);
            cba.AddAccount(account1);
            cba.AddAccount(account3);
            Bank westpac = new Bank(new List<string> {"WBC"}, "Westpac");
            CheckingAccount account2 = new CheckingAccount(654321, 4321, 100);
            westpac.AddAccount(account2);
            Bank nab = new Bank(new List<string> { "NAB" }, "National Australia Bank");
            Bank anz = new Bank(new List<string> { "ANZ" }, "Australia and New Zealand Banking Group");
            List<Bank> banksDatabase = new List<Bank> { cba, westpac, nab, anz };

            // Set the console title and text color
            Console.Title = "My ATM Application";
            Console.ForegroundColor = ConsoleColor.White;

            // Display the welcome message
            Utility.PrintColoredMessage("\n\n-----------------------------------Welcome to the ATM-----------------------------------\n", ConsoleColor.Green);

            //Sign in or create an account to sign in
            BankSelector bankSelector = new BankSelector(banksDatabase);
            Bank selectedBank = null;

            while (selectedBank == null)
            {
                selectedBank = bankSelector.SelectBank();
                if (selectedBank == null)
                {
                    Utility.PrintColoredMessage("Please select a valid bank by either abbreviated name or number", ConsoleColor.Red);
                }
               
            }

            Console.Clear();
            Utility.PrintColoredMessage("Welcome to " + selectedBank.Name + ".", ConsoleColor.Green);
            Utility.SleepWithDots(3000);

            Validator validator = new Validator();
            Account signedInAccount = validator.SignInOptions(selectedBank);

            //This option is for when user selects "back" in the sign in options. 
            while (signedInAccount == null)
            {   
                Console.Clear();
                selectedBank = bankSelector.SelectBank();
                signedInAccount = validator.SignInOptions(selectedBank);
            } 


            //Display account options
            AccountOptions accountOptions = new AccountOptions();
            bool isSignedOut = accountOptions.AccountOptionsMenu(signedInAccount, banksDatabase);
            
            //For the sign out option
            while (isSignedOut)
            {
                Console.Clear();
               signedInAccount = validator.SignInOptions(selectedBank);
               while (signedInAccount == null)
                {
                    Console.Clear();
                    selectedBank = bankSelector.SelectBank();
                    signedInAccount = validator.SignInOptions(selectedBank);
                }
               isSignedOut = accountOptions.AccountOptionsMenu(signedInAccount, banksDatabase);
            }

            
            
            
        }
    }
}
