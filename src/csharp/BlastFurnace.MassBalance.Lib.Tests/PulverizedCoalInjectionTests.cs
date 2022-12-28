using System;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class PulverizedCoalInjectionTests
{
    [Fact]
    public void CheckValidInitialization()
    {
        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(100, WeightUnits.metricTon));

        using (new AssertionScope())
        {
            pci.Should().NotBeNull();
            pci.CContent.Value.Should().Be(90);
            pci.Weight.Value.Should().Be(100);
        }
    }

    [Fact]
    public void CheckInitializationWithJustPercentual()
    {
        var pci = new PulverizedCoalInjection(new Percentual(90));

        using (new AssertionScope())
        {
            pci.Should().NotBeNull();
            pci.CContent.Value.Should().Be(90);
            pci.Weight.Value.Should().Be(0);
            pci.Weight.Unit.Should().Be(WeightUnits.metricTon);
        }
    }

    [Fact]
    public void CheckPCIRateMaximum()
    {
        var hotMetal = new HotMetal(new Weight(155, WeightUnits.metricTon), new Percentual(95), new Percentual(4));
        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(10, WeightUnits.metricTon));

        using (new AssertionScope())
        {
            pci.MaximumPCIRate(hotMetal).Should().BeApproximately(253.333, 0.001);
        }
    }

    [Fact]
    public void CheckPCIRateMaximumWithNullHotMetal()
    {
        HotMetal? hotmetal = null;
        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(10, WeightUnits.metricTon));

        var act = () => pci.MaximumPCIRate(hotmetal);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(100, WeightUnits.metricTon));
        pci.ToString().Should().Be("{\r\n  \"CContent\": {\r\n    \"Value\": 90.0\r\n  },\r\n  \"Weight\": {\r\n    \"Value\": 100.0,\r\n    \"Unit\": \"metricTon\"\r\n  }\r\n}");
    }
}
