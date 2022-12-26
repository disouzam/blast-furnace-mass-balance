using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class IronOreTests
{
    [Fact]
    public void CheckValidInitialization()
    {
        var ironOre1 = new IronOre(new Percentual(70), new Percentual(100));

        using (new AssertionScope())
        {
            ironOre1.Should().NotBeNull();
            ironOre1.FeContent.Value.Should().Be(70);
            ironOre1.Proportion.Value.Should().Be(100);
        }
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var ironOre1 = new IronOre(new Percentual(70), new Percentual(100));
        ironOre1.ToString().Should().Be("{\r\n  \"FeContent\": {\r\n    \"Value\": 70.0\r\n  },\r\n  \"Proportion\": {\r\n    \"Value\": 100.0\r\n  }\r\n}");
    }
}
