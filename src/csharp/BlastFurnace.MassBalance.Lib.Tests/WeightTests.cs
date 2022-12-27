using System;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class WeightTests
{
    [Fact]
    public void ValidWeightInKg()
    {
        var newWeight = new Weight(1000, WeightUnits.kilogram);

        using (new AssertionScope())
        {
            newWeight.Value.Should().Be(1000);
            newWeight.Unit.Should().Be(WeightUnits.kilogram);
        }
    }

    [Fact]
    public void ZeroWeightInKg()
    {
        var newWeight = new Weight(0, WeightUnits.kilogram);

        using (new AssertionScope())
        {
            newWeight.Value.Should().Be(0);
            newWeight.Unit.Should().Be(WeightUnits.kilogram);
        }
    }

    [Fact]
    public void NegativeWeightMustThrowException()
    {
        Action act = () => _ = new Weight(-10, WeightUnits.kilogram);

        act.Should().
            Throw<ArgumentOutOfRangeException>().
            WithMessage("Value can't be negative! (Parameter 'value')");

    }

    [Fact]
    public void CheckStringRepresentation()
    {
        using (new AssertionScope())
        {
            var newWeight = new Weight(1000, WeightUnits.kilogram);
            newWeight.ToString().Should().Be("{\r\n  \"Value\": 1000.0,\r\n  \"Unit\": \"kilogram\"\r\n}");

            var newWeight2 = new Weight(575, WeightUnits.metricTon);
            newWeight2.ToString().Should().Be("{\r\n  \"Value\": 575.0,\r\n  \"Unit\": \"metricTon\"\r\n}");
        }
    }
}
