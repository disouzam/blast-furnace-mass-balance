using System;

namespace BlastFurnace.MassBalance.ConsoleApp;

static class Program
{
    static void Main()
    {
        var header = new Header();
        Console.WriteLine("Blast Furnace Mass Balance Application");

        Console.WriteLine(header.DescriptiveText);
    }
}
