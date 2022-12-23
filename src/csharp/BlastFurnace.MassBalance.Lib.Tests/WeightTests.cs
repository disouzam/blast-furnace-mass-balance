using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class WeightTests
{
    [Fact]
    public void ValidWeightInKg()
    {
        var newWeight = new Weight(1000, WeightUnits.kilogram);

        newWeight.Value.Should().Be(1000);
        newWeight.Unit.Should().Be(WeightUnits.kilogram);
    }

    [Fact]
    public void ZeroWeightInKg()
    {
        var newWeight = new Weight(0, WeightUnits.kilogram);

        newWeight.Value.Should().Be(0);
        newWeight.Unit.Should().Be(WeightUnits.kilogram);
    }

    [Fact]
    public void NegativeWeightMustThrowException()
    {
        Action act = () => _ = new Weight(-10, WeightUnits.kilogram);

        act.Should().
            Throw<ArgumentOutOfRangeException>().
            WithMessage("Value can't be negative! (Parameter 'value')");

    }
}
