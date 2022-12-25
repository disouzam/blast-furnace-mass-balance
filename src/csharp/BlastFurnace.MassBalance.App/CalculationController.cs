using System;

namespace BlastFurnace.MassBalance.ConsoleApp
{
    internal static class CalculationController
    {
        public static void Run()
        {
            var selectedChoice = Console.ReadLine();
            while (selectedChoice?.ToLower() == "Y")
            {

                selectedChoice = Console.ReadLine();
            }
        }
    }
}
