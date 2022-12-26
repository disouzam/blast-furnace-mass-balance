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

            var ironOreBlend = GetIronOreBlend();
            Console.WriteLine(ironOreBlend.ToString());

            var cokeBlend = GetCokeBlend();
            Console.WriteLine(cokeBlend.ToString());

            Console.WriteLine("Deseja executar o programa novamente?");
            Console.WriteLine("Opções: (s)sim (n)nao?");
            selectedChoice = Console.ReadLine();
        } while (selectedChoice?.ToUpper() == "Y" || (selectedChoice?.ToUpper() == "S"));
    }

    private static HotMetal GetHotMetalInfo()
    {
        Console.WriteLine("Informe a quantidade de gusa que deve ser produzido (em toneladas):");
        var reading = Console.ReadLine();

        double weightReading;
        while (!double.TryParse(reading, out weightReading))
        {
            Console.WriteLine("Valor incorreto! Digite novamente...");
            reading = Console.ReadLine();
        }

        Console.WriteLine("Informe o teor de ferro e carbono no gusa que será produzido.");
        Console.WriteLine("Teor de ferro:");

        reading = Console.ReadLine();

        double fePercentageReading;
        while (!double.TryParse(reading, out fePercentageReading))
        {
            Console.WriteLine("Valor incorreto! Digite novamente...");
            reading = Console.ReadLine();
        }

        Console.WriteLine("Teor de carbono:");

        reading = Console.ReadLine();

        double cPercentageReading;
        while (!double.TryParse(reading, out cPercentageReading))
        {
            Console.WriteLine("Valor incorreto! Digite novamente...");
            reading = Console.ReadLine();
        }

        var hotMetal = new HotMetal(new Weight(weightReading, WeightUnits.metricTon), new Percentual(fePercentageReading), new Percentual(cPercentageReading));

        return hotMetal;
    }

    private static IronOreBlend GetIronOreBlend()
    {
        Console.WriteLine("Quantos tipos de minérios serão utilizados (máximo de 3)?");
        var reading = Console.ReadLine();

        var validReading = double.TryParse(reading, out var numberOfOres);
        while (!validReading || numberOfOres > 3)
        {
            if (!validReading)
            {
                Console.WriteLine("Valor incorreto! Digite novamente...");
            }

            if (numberOfOres > 3)
            {
                Console.WriteLine("Essa aplicação só processa até 3 tipos de minérios! Digite novamente...");
            }
            reading = Console.ReadLine();
            validReading = double.TryParse(reading, out numberOfOres);
        }

        var ironOreBlend = new IronOreBlend();

        double feContent;
        if (numberOfOres > 1)
        {
            for (var counter = 0; counter < numberOfOres; counter++)
            {
                Console.WriteLine($"Informe a proporção do minério #{counter} e o teor de ferro nele.");

                Console.WriteLine("Proporção:");
                reading = Console.ReadLine();

                validReading = double.TryParse(reading, out var proportion);
                while (!validReading || proportion < 0 || proportion > 100)
                {
                    if (!validReading)
                    {
                        Console.WriteLine("Valor incorreto! Digite novamente...");
                    }

                    if (proportion < 0 || proportion > 100)
                    {
                        Console.WriteLine("A proporção de um minério deve estar no intervalo [0,100] e a soma das proporções deve ser menor ou igual a 100%! Digite novamente...");
                    }
                    reading = Console.ReadLine();
                    validReading = double.TryParse(reading, out proportion);
                }

                Console.WriteLine("Teor de ferro:");
                reading = Console.ReadLine();

                validReading = double.TryParse(reading, out feContent);
                while (!validReading || feContent < 0 || feContent > 100)
                {
                    if (!validReading)
                    {
                        Console.WriteLine("Valor incorreto! Digite novamente...");
                    }

                    if (feContent < 0 || feContent > 100)
                    {
                        Console.WriteLine("O teor de Fe do minério é um valor no intervalo [0,100] mas tipicamente inferior a 77% (composição da wustita) ! Digite novamente...");
                    }
                    reading = Console.ReadLine();
                    validReading = double.TryParse(reading, out feContent);
                }

                var ironOre = new IronOre(new Percentual(feContent), new Percentual(proportion));
                ironOreBlend.Add(ironOre);
            }
        }
        else
        {
            Console.WriteLine("Informe o teor de ferro no minério.");
            reading = Console.ReadLine();
            validReading = double.TryParse(reading, out feContent);

            while (!validReading || feContent < 0 || feContent > 100)
            {
                if (!validReading)
                {
                    Console.WriteLine("Valor incorreto! Digite novamente...");
                }

                if (feContent < 0 || feContent > 100)
                {
                    Console.WriteLine("O teor de Fe do minério é um valor no intervalo [0,100] mas tipicamente inferior a 77% (composição da wustita) ! Digite novamente...");
                }
                reading = Console.ReadLine();
                validReading = double.TryParse(reading, out feContent);
            }

            var ironOre = new IronOre(new Percentual(feContent), new Percentual(100));
            ironOreBlend.Add(ironOre);
        }

        return ironOreBlend;
    }

    private static CokeBlend GetCokeBlend()
    {
        Console.WriteLine("Quantos tipos de coques serão utilizados (máximo de 3)?");
        var reading = Console.ReadLine();

        var validReading = double.TryParse(reading, out var numberOfCokes);
        while (!validReading || numberOfCokes > 3)
        {
            if (!validReading)
            {
                Console.WriteLine("Valor incorreto! Digite novamente...");
            }

            if (numberOfCokes > 3)
            {
                Console.WriteLine("Essa aplicação só processa até 3 tipos de coques! Digite novamente...");
            }
            reading = Console.ReadLine();
            validReading = double.TryParse(reading, out numberOfCokes);
        }

        var cokeBlend = new CokeBlend();

        double cContent;
        if (numberOfCokes > 1)
        {
            for (var counter = 0; counter < numberOfCokes; counter++)
            {
                Console.WriteLine($"Informe a proporção do coque #{counter} e o teor de carbono fixo nele.");

                Console.WriteLine("Proporção:");
                reading = Console.ReadLine();

                validReading = double.TryParse(reading, out var proportion);
                while (!validReading || proportion < 0 || proportion > 100)
                {
                    if (!validReading)
                    {
                        Console.WriteLine("Valor incorreto! Digite novamente...");
                    }

                    if (proportion < 0 || proportion > 100)
                    {
                        Console.WriteLine("A proporção de um coque deve estar no intervalo [0,100] e a soma das proporções deve ser menor ou igual a 100%! Digite novamente...");
                    }
                    reading = Console.ReadLine();
                    validReading = double.TryParse(reading, out proportion);
                }

                Console.WriteLine("Teor de carbono fixo:");
                reading = Console.ReadLine();

                validReading = double.TryParse(reading, out cContent);
                while (!validReading || cContent < 0 || cContent > 100)
                {
                    if (!validReading)
                    {
                        Console.WriteLine("Valor incorreto! Digite novamente...");
                    }

                    if (cContent < 0 || cContent > 100)
                    {
                        Console.WriteLine("O teor de C fixo do coque é um valor no intervalo [0,100]! Digite novamente...");
                    }
                    reading = Console.ReadLine();
                    validReading = double.TryParse(reading, out cContent);
                }

                var coke = new Coke(new Percentual(cContent), new Percentual(proportion));
                cokeBlend.Add(coke);
            }
        }
        else
        {
            Console.WriteLine("Informe o teor de carbono fixo no coque.");
            reading = Console.ReadLine();
            validReading = double.TryParse(reading, out cContent);

            while (!validReading || cContent < 0 || cContent > 100)
            {
                if (!validReading)
                {
                    Console.WriteLine("Valor incorreto! Digite novamente...");
                }

                if (cContent < 0 || cContent > 100)
                {
                    Console.WriteLine("O teor de C fixo do coque é um valor no intervalo [0,100]! Digite novamente...");
                }
                reading = Console.ReadLine();
                validReading = double.TryParse(reading, out cContent);
            }

            var coke = new Coke(new Percentual(cContent), new Percentual(100));
            cokeBlend.Add(coke);
        }

        return cokeBlend;
    }
}
