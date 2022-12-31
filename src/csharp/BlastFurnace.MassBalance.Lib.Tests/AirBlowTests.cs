using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class AirBlowTests
{
    [Fact]
    public void CheckInitialization()
    {
        var airBlow = new AirBlow(new Percentual(21));
        airBlow.O2Content.Value.Should().Be(21);
    }

    [Fact]
    public void CheckRequiredAirVolume()
    {
        var airBlow = new AirBlow(new Percentual(21));
        var hotMetal = new HotMetal(new Weight(155, WeightUnits.metricTon), new Percentual(95), new Percentual(4));

        airBlow.GetRequiredAirVolume(hotMetal).Value.Should().BeApproximately(157066.666, 0.001);
        airBlow.GetRequiredAirVolume(hotMetal).Unit.Should().Be(VolumeUnits.Nm3);
    }

    [Fact]
    public void CheckAirBlowRateRequiredPerMinute()
    {
        var airBlow = new AirBlow(new Percentual(21));
        var hotMetal = new HotMetal(new Weight(155, WeightUnits.metricTon), new Percentual(95), new Percentual(4));

        airBlow.AirBlowRateRequiredPerMinute(hotMetal).Should().BeApproximately(109.074, 0.001);
    }

    [Fact]
    public void CheckRequiredAirVolumeNullHotMetal()
    {
        var airBlow = new AirBlow(new Percentual(21));
        HotMetal? hotMetal = null;

#pragma warning disable CS8604 // Possible null reference argument.
        var act = () => airBlow.GetRequiredAirVolume(hotMetal);
#pragma warning restore CS8604 // Possible null reference argument.
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CheckAirBlowRateRequiredPerMinuteNullHotMetal()
    {
        var airBlow = new AirBlow(new Percentual(21));
        HotMetal? hotMetal = null;

#pragma warning disable CS8604 // Possible null reference argument.
        var act = () => airBlow.AirBlowRateRequiredPerMinute(hotMetal);
#pragma warning restore CS8604 // Possible null reference argument.
        act.Should().Throw<ArgumentNullException>()
           .WithMessage("Value cannot be null. (Parameter 'hotMetal')")
           .Which?.TargetSite?.Name.Should().Be(nameof(AirBlow.AirBlowRateRequiredPerMinute));
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var airBlow = new AirBlow(new Percentual(21));
        airBlow.ToString().Should().Be("{\r\n  \"O2Content\": {\r\n    \"Value\": 21.0\r\n  }\r\n}");
    }
}
