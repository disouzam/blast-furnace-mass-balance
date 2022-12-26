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
    public void CheckStringRepresentation()
    {
        var airBlow = new AirBlow(new Percentual(21));
        airBlow.ToString().Should().Be("{\r\n  \"O2Content\": {\r\n    \"Value\": 21.0\r\n  }\r\n}");
    }
}
