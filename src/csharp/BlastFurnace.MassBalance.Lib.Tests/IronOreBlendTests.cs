using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class IronOreBlendTests
{
    [Fact]
    public void CreateNewObject()
    {
        var ironOreBlend = new IronOreBlend();
        ironOreBlend.Should().NotBeNull();
    }

    [Fact]
    public void AddOneIronOreToAnEmptyIronOreBlend()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(100));

        var act = () => ironOreBlend.Add(ironOre);

        act.Should().NotThrow();
        ironOreBlend.Should().NotBeNull();
    }

    [Fact]
    public void AddIronOresAndExceedHundredPercent()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(80));
        ironOreBlend.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(75));
        var act = () => ironOreBlend.Add(ironOre2);

        act.Should().Throw<InvalidOperationException>().WithMessage("Total proportion must be at a maximum of 100%.");
        ironOreBlend.Should().NotBeNull();
        ironOreBlend.TotalProportion.Should().Be(80);
    }

    [Fact]
    public void AddOneIronOreAndNormalizeProportions()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(75.15));

        ironOreBlend.Add(ironOre);
        ironOreBlend.NormalizeProportions();
        ironOreBlend.TotalProportion.Should().Be(100);
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

        ironOreBlend.ToString().Should().Be("{\r\n  \"IronOres\": [\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 70.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 25.0\r\n      }\r\n    },\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 65.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 35.0\r\n      }\r\n    },\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 60.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 40.0\r\n      }\r\n    }\r\n  ],\r\n  \"TotalProportion\": 100.0\r\n}");
    }
}
