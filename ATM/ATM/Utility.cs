using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM
{
    public class Utility
    {
        public static string GetSecretInput(string prompt)
        {
            Console.Write(prompt + "\n");
            StringBuilder input = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (inputKey.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                        // Move the cursor back, overwrite the last asterisk with a space, and move the cursor back again
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine(); // Move to the next line after input is done
            return input.ToString();
        }

        public static void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PressEnterToContinue()
        {
            Console.WriteLine("\nPress Enter to continue...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
            }
        }

        public static void SleepWithDots(int milliseconds)
        {
            int dotsCount = 0;
            while (milliseconds > 0)
            {
                Console.Write(".");
                Thread.Sleep(500); // Sleep for 500 milliseconds for each dot
                milliseconds -= 500;
                dotsCount++;
                if (dotsCount % 10 == 0) // Print newline after every 10 dots
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine(); // Ensure newline after sleeping
        }
    }
}
