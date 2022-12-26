using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class AboutTests
{
    [Fact]
    public void CheckStringMessageInPortuguese()
    {
        var i = 0;
        var smallestLength = About.Description.Length < expectedMessageInPortuguese.Length ? About.Description.Length : expectedMessageInPortuguese.Length;

        while (i < smallestLength)
        {
#pragma warning disable IDE0057 // Use range operator
            About.Description.Substring(0, i).Should().Be(expectedMessageInPortuguese.Substring(0, i));
#pragma warning restore IDE0057 // Use range operator
            i++;
        }

        using (new AssertionScope())
        {
            About.Description.Length.Should().Be(expectedMessageInPortuguese.Length);
            About.Description.Should().Be(expectedMessageInPortuguese);
        }
    }

    [Fact]
    public void CheckAppTitle()
    {
        About.AppTitle.Should().Be("Blast Furnace Mass Balance Application");
    }

    private static readonly string expectedMessageInPortuguese = "Este programa realiza um cálculo de carga simplificado para um alto forno, feito com base nos exercícios feitos em sala de aula e, também, com base nos conhecimentos do grupo, tanto na montagem da linha de pensamento como na montagem do programa usando a linguagem Pascal." +
"\r\n" + "\r\n" +
"O programa considera o carregamento de até 3 tipos de minérios (estes podem ser os minérios, pelotas e sínteres)." +
"\r\n" + "\r\n" +
"A partir de uma proporção em peso fixa, a carga de minérios é calculada. Calcula-se também o teor médio de ferro contido nela." +
"\r\n" + "\r\n" +
"A carga de coque é feita de forma similar: estipula-se a proporção dos diferentes tipos de coque (no máximo 3) e calcula-se a massa de cada um delespara atender ao processo." +
"\r\n" + "\r\n" +
"Pode-se calcular a quantidade de PCI máxima a se injetar, e a partir dela, estipular um valor a ser usado." +
"\r\n" + "\r\n" +
"O sopro pode ser calculado, considerando-se a necessidade de oxigênio e o seu teor no sopro." +
"\r\n" + "\r\n" +
"Os cálculos são feitos com base nas considerações de que todo o ferro carregado é reduzido e se incorpora ao gusa líquido e a reação de redução da wustita a ferro se processam integralmente a 900 °C, de forma que a reação de redução seja:" +
"\r\n" + "\r\n" +
"FeO +3,12CO = Fe + 2,12 CO + CO2." +
"\r\n" + "\r\n" +
"Após o uso do programa, pode-se armazenar os dados em um arquivo texto (Calculo_de_carga.doc)" +
"\r\n";
}
