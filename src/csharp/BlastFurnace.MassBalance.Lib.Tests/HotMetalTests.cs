using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class HotMetalTests
{
    [Fact]
    public void CheckValidInitialization()
    {
        var hotMetal = new HotMetal(new Weight(1, WeightUnits.kilogram), new Percentual(95), new Percentual(5));

        hotMetal.Should().NotBeNull();
        hotMetal.Weight.Value.Should().Be(1);
        hotMetal.Weight.Unit.Should().Be(WeightUnits.kilogram);
        hotMetal.FePercent.Value.Should().Be(95);
        hotMetal.CPercent.Value.Should().Be(5);

        var hotMetal2 = new HotMetal(new Weight(1, WeightUnits.metricTon), new Percentual(94), new Percentual(5));

        hotMetal2.Should().NotBeNull();
        hotMetal2.Weight.Value.Should().Be(1);
        hotMetal2.Weight.Unit.Should().Be(WeightUnits.metricTon);
        hotMetal2.FePercent.Value.Should().Be(94);
        hotMetal2.CPercent.Value.Should().Be(5);
    }

    [Fact]
    public void CheckInvalidInitialization()
    {
        HotMetal hotMetal;
        var act = () => hotMetal = new HotMetal(new Weight(1, WeightUnits.kilogram), new Percentual(100), new Percentual(5));

        act.Should().Throw<InvalidOperationException>().WithMessage("Composition should not exceed 100%! Current value is at 105 %.");
    }
}
