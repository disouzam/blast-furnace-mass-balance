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
    public void CheckStringRepresentation()
    {
        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(100, WeightUnits.metricTon));
        pci.ToString().Should().Be("{\r\n  \"CContent\": {\r\n    \"Value\": 90.0\r\n  },\r\n  \"Weight\": {\r\n    \"Value\": 100.0,\r\n    \"Unit\": \"metricTon\"\r\n  }\r\n}");
    }
}
