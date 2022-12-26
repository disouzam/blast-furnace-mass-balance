using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class CokeTests
{
    [Fact]
    public void CheckValidInitialization()
    {
        var coke1 = new Coke(new Percentual(90), new Percentual(100));
        coke1.Should().NotBeNull();
        coke1.CContent.Value.Should().Be(90);
        coke1.Proportion.Value.Should().Be(100);
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var coke1 = new Coke(new Percentual(90), new Percentual(100));
        coke1.ToString().Should().Be("{\r\n  \"CContent\": {\r\n    \"Value\": 90.0\r\n  },\r\n  \"Proportion\": {\r\n    \"Value\": 100.0\r\n  }\r\n}");
    }
}
