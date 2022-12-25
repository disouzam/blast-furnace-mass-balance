using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class HotMetalTests
{
    private readonly HotMetal hotMetal;

    private readonly HotMetal hotMetal2;

    public HotMetalTests()
    {
        hotMetal = new HotMetal(new Weight(1, WeightUnits.kilogram), new Percentual(95), new Percentual(5));
        hotMetal2 = new HotMetal(new Weight(1, WeightUnits.metricTon), new Percentual(94), new Percentual(5));
    }

    [Fact]
    public void CheckValidInitialization()
    {
        hotMetal.Should().NotBeNull();
        hotMetal.Weight.Value.Should().Be(1);
        hotMetal.Weight.Unit.Should().Be(WeightUnits.kilogram);
        hotMetal.FePercent.Value.Should().Be(95);
        hotMetal.CPercent.Value.Should().Be(5);

        hotMetal2.Should().NotBeNull();
        hotMetal2.Weight.Value.Should().Be(1);
        hotMetal2.Weight.Unit.Should().Be(WeightUnits.metricTon);
        hotMetal2.FePercent.Value.Should().Be(94);
        hotMetal2.CPercent.Value.Should().Be(5);
    }

    [Fact]
    public void CheckInvalidInitialization()
    {
        HotMetal hotMetal3;
        var act = () => hotMetal3 = new HotMetal(new Weight(1, WeightUnits.kilogram), new Percentual(100), new Percentual(5));

        act.Should().Throw<InvalidOperationException>().WithMessage("Composition should not exceed 100%! Current value is at 105 %.");
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        hotMetal.ToString().Should().Be("{\r\n  \"Weight\": {\r\n    \"Value\": 1.0,\r\n    \"Unit\": \"kilogram\"\r\n  },\r\n  \"FePercent\": {\r\n    \"Value\": 95.0\r\n  },\r\n  \"CPercent\": {\r\n    \"Value\": 5.0\r\n  }\r\n}");
    }
}
