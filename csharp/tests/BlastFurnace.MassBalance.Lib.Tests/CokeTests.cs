using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class CokeTests
{
    [Fact]
    public void CheckValidInitialization()
    {
        var coke1 = new Coke(new Percentual(90), new Percentual(100));

        using (new AssertionScope())
        {
            coke1.Should().NotBeNull();
            coke1.CContent.Value.Should().Be(90);
            coke1.Proportion.Value.Should().Be(100);
            coke1.Weight.Value.Should().Be(0);
            coke1.Weight.Unit.Should().Be(WeightUnits.metricTon);
        }

        var coke2 = new Coke(new Percentual(88), new Percentual(85), new Weight(4500, WeightUnits.kilogram));

        using (new AssertionScope())
        {
            coke2.Should().NotBeNull();
            coke2.CContent.Value.Should().Be(88);
            coke2.Proportion.Value.Should().Be(85);
            coke2.Weight.Value.Should().Be(4500);
            coke2.Weight.Unit.Should().Be(WeightUnits.kilogram);
        }
    }

    [Fact]
    public void CheckSetWeight()
    {
        var coke1 = new Coke(new Percentual(90), new Percentual(100));

        using (new AssertionScope())
        {
            coke1.Should().NotBeNull();

            coke1.Weight.Value.Should().Be(0);
            coke1.Weight.Unit.Should().Be(WeightUnits.metricTon);

            coke1.SetWeight(4500, WeightUnits.kilogram);

            coke1.Weight.Value.Should().Be(4500);
            coke1.Weight.Unit.Should().Be(WeightUnits.kilogram);

            coke1.CContent.Value.Should().Be(90);
            coke1.Proportion.Value.Should().Be(100);
        }
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var coke1 = new Coke(new Percentual(90), new Percentual(100));
        coke1.ToString().Should().Be("{\r\n  \"CContent\": {\r\n    \"Value\": 90.0\r\n  },\r\n  \"Proportion\": {\r\n    \"Value\": 100.0\r\n  },\r\n  \"Weight\": {\r\n    \"Value\": 0.0,\r\n    \"Unit\": \"metricTon\"\r\n  }\r\n}");
    }
}
