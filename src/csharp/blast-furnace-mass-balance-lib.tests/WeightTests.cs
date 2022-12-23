using System;

using FluentAssertions;

using Xunit;

namespace blast_furnace_mass_balance_lib.tests;

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
        Action act = () => new Weight(-10, WeightUnits.kilogram);

        act.Should().
            Throw<ArgumentOutOfRangeException>().
            WithMessage("Specified argument was out of the range of valid values. (Parameter 'value can't be negative!')");

    }
}