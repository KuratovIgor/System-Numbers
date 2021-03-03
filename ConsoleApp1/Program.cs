using System;
using LibSystemNumbers;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number system: ");
            int firstBase = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number: ");
            string stringNumbers = Console.ReadLine();

            Console.Write("Enter second number system: ");
            int secondBase = Convert.ToInt32(Console.ReadLine());

            char[] result = Number.TranslateToSystemNumbers(stringNumbers, firstBase, secondBase);

            Console.WriteLine(result);
        }
    }
}
