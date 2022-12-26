using System;

namespace BlastFurnace.MassBalance.ConsoleApp;

static class Program
{
    static void Main()
    {
        var header = new Header();
        Console.WriteLine(header.AppTitle);
        Console.WriteLine();
        Console.WriteLine(header.DescriptiveText);

        CalculationController.Run();
    }
}
