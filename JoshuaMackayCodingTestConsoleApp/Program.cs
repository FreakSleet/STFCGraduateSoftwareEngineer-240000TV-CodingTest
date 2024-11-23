using JoshuaMackaySTFCCodingTest;

namespace JoshuaMackayCodingTestConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            OrderCostCalculator calculator = new();

            // Read first order
            Console.WriteLine("Please input order, or only press enter to exit:");
            string? orders = Console.ReadLine();

            // While orders are inputted, print total order and ask for another order
            while (!string.IsNullOrEmpty(orders))
            {
                Console.WriteLine(calculator.CalculateCost(orders));

                Console.WriteLine("Please input another order, or only press enter to exit:");
                orders = Console.ReadLine();
            }

            Console.WriteLine("Thank you.");
        }
    }
} 