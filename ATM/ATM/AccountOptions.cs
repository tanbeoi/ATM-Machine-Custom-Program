using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM
{
    public class AccountOptions
    {
        public AccountOptions()
        {
        }

        public bool AccountOptionsMenu(Account account, List<Bank> banksDatabase)
        {   
            bool isSignedOut = false;
            Console.Clear();
            Console.WriteLine("Please select an option: \n1. Deposit \n2. Withdraw \n3. Transfer \n4. Check balance \n5. Check transactions history\n6. Sign out\n\nYour Option:");
            string Input;
            int InputOption;

            while (true)
            {
                Input = Console.ReadLine().Trim();

                if (int.TryParse(Input, out InputOption) && InputOption > 0 && InputOption < 7)
                {
                    break;
                }
                else
                {
                    Utility.PrintColoredMessage("Invalid input. Please enter a valid option.", ConsoleColor.Red);
                    Utility.SleepWithDots(1000);
                    return AccountOptionsMenu(account, banksDatabase);
                }
            }

            switch (InputOption)
            {
                case 1:

                    Console.WriteLine("Please enter the amount you would like to deposit: ");

                    int DepositAmount;
                    while (true)
                    {
                        string input = Console.ReadLine().Trim();
                        if (int.TryParse(input, out DepositAmount) && DepositAmount > 0)
                        {
                            break;
                        }
                        else
                        {
                        
                            Utility.PrintColoredMessage("Invalid input. Please enter a valid positive number.", ConsoleColor.Red);
                            Utility.SleepWithDots(1000);
                            return AccountOptionsMenu(account, banksDatabase);
                        }
                    }

                    
                    Console.Clear();
                    Console.WriteLine(account.Deposit(DepositAmount));
                    Utility.PressEnterToContinue();
                    return AccountOptionsMenu(account, banksDatabase);

                case 2:
                  
                    Console.WriteLine("Please enter the amount you would like to withdraw: ");

                    int WithdrawAmount;
                    while (true)
                    {
                        string input = Console.ReadLine().Trim();
                        if (int.TryParse(input, out WithdrawAmount) && WithdrawAmount > 0)
                        {
                            break;
                        }
                        else
                        { 
                            Utility.PrintColoredMessage("Invalid input. Please enter a valid positive number.", ConsoleColor.Red);
                            Utility.SleepWithDots(1000);
                            return AccountOptionsMenu(account, banksDatabase);
                        }
                    }

                    string withdrawalResult = account.Withdraw(WithdrawAmount);
                    if (withdrawalResult == null)
                    {
                        Utility.PrintColoredMessage("Insufficient funds. Please try again.", ConsoleColor.Red);
                        Utility.SleepWithDots(1000);
                        return AccountOptionsMenu(account, banksDatabase);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(withdrawalResult);
                        Utility.PressEnterToContinue();
                        return AccountOptionsMenu(account, banksDatabase);
                    }

                case 3:
           
                    Console.WriteLine("Please enter the amount you would like to transfer: ");

                    int TransferAmount;
                    while (true)
                    {
                        string input = Console.ReadLine().Trim();
                        if (int.TryParse(input, out TransferAmount) && TransferAmount > 0)
                        {
                            break;
                        }
                        else
                        {
                            Utility.PrintColoredMessage("Invalid input. Please enter a valid positive number.", ConsoleColor.Red);
                            Utility.SleepWithDots(1000);
                            return AccountOptionsMenu(account, banksDatabase);
                        }
                    }

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

                    Console.WriteLine("\nPlease enter the account number you would like to transfer to (6 digits): ");

                    int DestinationAccountNum;
                    while (true)
                    {
                        string input = Console.ReadLine().Trim();
                        if (int.TryParse(input, out DestinationAccountNum) && input.Length == 6)
                        {
                            break;
                        }
                        else
                        {
                            Utility.PrintColoredMessage("Invalid input. Please enter a valid 6-digit account number.", ConsoleColor.Red);
                            Utility.SleepWithDots(1000);
                            return AccountOptionsMenu(account, banksDatabase);
                        }
                    }

                    Account? destinationAccount = selectedBank.GetTransferAccount(DestinationAccountNum);
                    if (destinationAccount == null)
                    {
                                
                        Utility.PrintColoredMessage("Account not found. Please try again.", ConsoleColor.Red);
                        Utility.SleepWithDots(1000);
                        return AccountOptionsMenu(account, banksDatabase);
                    }
                    else if (TransferAmount > account.Balance)
                    {          
                        Utility.PrintColoredMessage("Insufficient funds. Please try again.", ConsoleColor.Red);
                        Utility.SleepWithDots(1000);
                        return AccountOptionsMenu(account, banksDatabase);
                    } 
                    else 
                    {
                        Console.Clear();
                        Console.WriteLine(account.Transfer(TransferAmount, destinationAccount));
                        Utility.PressEnterToContinue();
                        return AccountOptionsMenu(account, banksDatabase);
                    }

                  

                case 4:
                    Console.Clear();
                    Console.WriteLine("Your current balance is $" + account.Balance);
                    Utility.PressEnterToContinue();
                    return AccountOptionsMenu(account, banksDatabase);


                case 5:
                    Console.Clear();
                    Console.WriteLine("Transaction History: ");
                    Console.WriteLine(account.TransactionHistory());
                    Utility.PressEnterToContinue();
                    return AccountOptionsMenu(account, banksDatabase);


                case 6:
                    Console.Clear();
                    Console.WriteLine("You have signed out successfully.");
                    Utility.PressEnterToContinue();
                    isSignedOut = true;
                    return isSignedOut;


                default:
                 
                    Console.WriteLine("Invalid option. Please try again.");
                    Utility.SleepWithDots(1000);
                    return AccountOptionsMenu(account, banksDatabase);
            }
        }
    }
}
