using System;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class IronOreBlendTests
{
    [Fact]
    public void CreateNewObject()
    {
        var ironOreBlend = new IronOreBlend();

        using (new AssertionScope())
        {
            ironOreBlend.Should().NotBeNull("because no iron ore has been added.");
            ironOreBlend.IronOres.Count.Should().Be(0, "because no iron ore has been added.");
            ironOreBlend.AverageFeContent.Value.Should().Be(0);
        }
    }

    [Fact]
    public void AddOneIronOreToAnEmptyIronOreBlend()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(100));

        var act = () => ironOreBlend.Add(ironOre);

        using (new AssertionScope())
        {
            act.Should().NotThrow();
            ironOreBlend.Should().NotBeNull();
            ironOreBlend.TotalProportion.Value.Should().Be(100);
            ironOreBlend.AverageFeContent.Value.Should().Be(70);
        }

        var ironOres = ironOreBlend.IronOres;

        using (new AssertionScope())
        {
            ironOres.Should().NotBeNull();
            ironOres.Count.Should().Be(1);
            ironOres[0].Should().NotBeNull();
            ironOres[0].FeContent.Value.Should().Be(70);
            ironOres[0].Proportion.Value.Should().Be(100);
        }
    }

    [Fact]
    public void AddIronOresAndExceedHundredPercent()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(80));
        ironOreBlend.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(75));
        var act = () => ironOreBlend.Add(ironOre2);

        using (new AssertionScope())
        {
            act.Should().Throw<InvalidOperationException>().WithMessage("Total proportion must be at a maximum of 100%. Iron ore passed as parameter was not added to the blend.");
            ironOreBlend.Should().NotBeNull();
            ironOreBlend.TotalProportion.Value.Should().Be(80);
            ironOreBlend.AverageFeContent.Value.Should().Be(70);
        }

        var ironOres = ironOreBlend.IronOres;

        using (new AssertionScope())
        {
            ironOres.Should().NotBeNull();
            ironOres.Count.Should().Be(1);
            ironOres[0].Should().NotBeNull();
            ironOres[0].FeContent.Value.Should().Be(70);
            ironOres[0].Proportion.Value.Should().Be(80);
        }
    }

    [Fact]
    public void AddOneIronOreAndNormalizeProportions()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(75.15));

        ironOreBlend.Add(ironOre);
        ironOreBlend.NormalizeProportions();

        using (new AssertionScope())
        {
            ironOreBlend.TotalProportion.Value.Should().Be(100);
            ironOreBlend.AverageFeContent.Value.Should().Be(70);
        }

        var ironOres = ironOreBlend.IronOres;

        using (new AssertionScope())
        {
            ironOres.Should().NotBeNull();
            ironOres.Count.Should().Be(1);
            ironOres[0].Should().NotBeNull();
            ironOres[0].FeContent.Value.Should().Be(70);
            ironOres[0].Proportion.Value.Should().Be(100);
        }
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(25));
        ironOreBlend.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(35));
        ironOreBlend.Add(ironOre2);

        var ironOre3 = new IronOre(new Percentual(60), new Percentual(40));
        ironOreBlend.Add(ironOre3);

        ironOreBlend.ToString().Should().Be("{\r\n  \"IronOres\": [\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 70.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 25.0\r\n      }\r\n    },\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 65.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 35.0\r\n      }\r\n    },\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 60.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 40.0\r\n      }\r\n    }\r\n  ],\r\n  \"TotalProportion\": {\r\n    \"Value\": 100.0\r\n  },\r\n  \"AverageFeContent\": {\r\n    \"Value\": 64.25\r\n  }\r\n}");
        ironOreBlend.AverageFeContent.Value.Should().Be(64.25);
    }
}
