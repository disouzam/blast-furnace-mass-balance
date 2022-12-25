using System;

using BlastFurnace.MassBalance.Lib;

namespace BlastFurnace.MassBalance.ConsoleApp;

internal static class CalculationController
{
    public static void Run()
    {
        string? selectedChoice;
        do
        {
            var hotMetal = GetHotMetalInfo();
            Console.WriteLine(hotMetal.ToString());

            Console.WriteLine("Deseja executar o programa novamente?");
            Console.WriteLine("Opções: (s)sim (n)nao?");
            selectedChoice = Console.ReadLine();
        } while (selectedChoice?.ToUpper() == "Y" || (selectedChoice?.ToUpper() == "S"));
    }

    private static HotMetal GetHotMetalInfo()
    {
        Console.WriteLine("Informe a quantidade de gusa que deve ser produzido(em toneladas):");
        var reading = Console.ReadLine();

        double weightReading;
        while (!double.TryParse(reading, out weightReading))
        {
            Console.WriteLine("Valor incorreto! Digite novamente...");
        }

        Console.WriteLine("Informe o teor de ferro e carbono no gusa que será produzido.");
        Console.WriteLine("Teor de ferro:");

        reading = Console.ReadLine();

        double fePercentageReading;
        while (!double.TryParse(reading, out fePercentageReading))
        {
            Console.WriteLine("Valor incorreto! Digite novamente...");
        }

        Console.WriteLine("Teor de carbono:");

        reading = Console.ReadLine();

        double cPercentageReading;
        while (!double.TryParse(reading, out cPercentageReading))
        {
            Console.WriteLine("Valor incorreto! Digite novamente...");
        }

        var hotMetal = new HotMetal(new Weight(weightReading, WeightUnits.metricTon), new Percentual(fePercentageReading), new Percentual(cPercentageReading));

        return hotMetal;
    }
}