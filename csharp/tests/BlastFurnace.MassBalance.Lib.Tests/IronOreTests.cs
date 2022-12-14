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
            ironOre1.Weight.Value.Should().Be(0);
            ironOre1.Weight.Unit.Should().Be(WeightUnits.metricTon);
        }
    }

    [Fact]
    public void CheckValidInitializationWithWeight()
    {
        var ironOre1 = new IronOre(new Percentual(70), new Percentual(100), new Weight(150, WeightUnits.metricTon));

        using (new AssertionScope())
        {
            ironOre1.Should().NotBeNull();
            ironOre1.FeContent.Value.Should().Be(70);
            ironOre1.Proportion.Value.Should().Be(100);
            ironOre1.Weight.Value.Should().Be(150);
            ironOre1.Weight.Unit.Should().Be(WeightUnits.metricTon);
        }
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var ironOre1 = new IronOre(new Percentual(70), new Percentual(100));
        ironOre1.ToString().Should().Be("{\r\n  \"FeContent\": {\r\n    \"Value\": 70.0\r\n  },\r\n  \"Proportion\": {\r\n    \"Value\": 100.0\r\n  },\r\n  \"Weight\": {\r\n    \"Value\": 0.0,\r\n    \"Unit\": \"metricTon\"\r\n  }\r\n}");

        var ironOre2 = new IronOre(new Percentual(50), new Percentual(65), new Weight(135, WeightUnits.kilogram));
        ironOre2.ToString().Should().Be("{\r\n  \"FeContent\": {\r\n    \"Value\": 50.0\r\n  },\r\n  \"Proportion\": {\r\n    \"Value\": 65.0\r\n  },\r\n  \"Weight\": {\r\n    \"Value\": 135.0,\r\n    \"Unit\": \"kilogram\"\r\n  }\r\n}");
    }

    [Fact]
    public void CheckSetWeight()
    {
        var ironOre1 = new IronOre(new Percentual(70), new Percentual(100));
        var properties = ironOre1?.GetType()?.GetProperty(nameof(IronOre.Weight))?.GetAccessors(true);

        using (new AssertionScope())
        {
            properties.Should().NotBeNull();
            properties?.Length.Should().Be(2);

            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    if (prop.Name == $"set_{nameof(IronOre.Weight)}")
                    {
                        prop.IsPublic.Should().BeFalse($"Setter of {nameof(IronOre.Weight)} property of class {nameof(IronOre)} must be private!");
                    }

                    if (prop.Name == $"get_{nameof(IronOre.Weight)}")
                    {
                        prop.IsPublic.Should().BeTrue($"Getter of {nameof(IronOre.Weight)} property of class {nameof(IronOre)} must be public!");
                    }
                } 
            }

            ironOre1?.Weight.Value.Should().Be(0);
            ironOre1?.Weight.Unit.Should().Be(WeightUnits.metricTon);

            ironOre1?.SetWeight(500, WeightUnits.kilogram);

            ironOre1?.Weight.Value.Should().Be(500);
            ironOre1?.Weight.Unit.Should().Be(WeightUnits.kilogram);
        }
    }
}
