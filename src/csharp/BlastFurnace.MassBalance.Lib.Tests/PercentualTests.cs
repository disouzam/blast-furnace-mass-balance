using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class PercentualTests
{
    [Fact]
    public void CheckValidInitialization()
    {
        var randomGenerator = new Random();
        var randomValue = 100 * randomGenerator.NextDouble();

        var percentualValue = new Percentual(randomValue);
        percentualValue.Value.Should().Be(randomValue);

        var percentualValue2 = new Percentual(100);
        percentualValue2.Value.Should().Be(100);

        var percentualValue3 = new Percentual(0);
        percentualValue3.Value.Should().Be(0);
    }

    [Fact]
    public void CheckInvalidInitialization()
    {
        var randomGenerator = new Random();

        var randomValue = -100 * randomGenerator.NextDouble();
        var act = () => _ = new Percentual(randomValue);
        act.Should().Throw<ArgumentOutOfRangeException>().WithMessage($"Percentage value must belong to [0,100] interval. Current value ({randomValue}) is invalid! (Parameter 'value')");

        var randomValue2 = 100 + 100 * randomGenerator.NextDouble();
        var act2 = () => _ = new Percentual(randomValue2);
        act2.Should().Throw<ArgumentOutOfRangeException>().WithMessage($"Percentage value must belong to [0,100] interval. Current value ({randomValue2}) is invalid! (Parameter 'value')");
    }
}
