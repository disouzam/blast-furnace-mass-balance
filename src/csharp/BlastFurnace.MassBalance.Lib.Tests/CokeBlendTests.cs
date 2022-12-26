using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class CokeBlendTests
{
    [Fact]
    public void CreateNewObject()
    {
        var cokeBlend = new CokeBlend();
        cokeBlend.Should().NotBeNull();
    }

    [Fact]
    public void AddOneCokeToAnEmptyCokeBlend()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(100));

        var act = () => cokeBlend.Add(coke);

        act.Should().NotThrow();
        cokeBlend.Should().NotBeNull();
        cokeBlend.TotalProportion.Should().Be(100);

        var cokes = cokeBlend.Cokes;
        cokes.Should().NotBeNull();
        cokes.Count.Should().Be(1);
        cokes[0].Should().NotBeNull();
        cokes[0].CContent.Value.Should().Be(95);
        cokes[0].Proportion.Value.Should().Be(100);
    }

    [Fact]
    public void AddCokesAndExceedHundredPercent()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(80));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(75));
        var act = () => cokeBlend.Add(coke2);

        act.Should().Throw<InvalidOperationException>().WithMessage("Total proportion must be at a maximum of 100%.");
        cokeBlend.Should().NotBeNull();
        cokeBlend.TotalProportion.Should().Be(80);

        var cokes = cokeBlend.Cokes;
        cokes.Should().NotBeNull();
        cokes.Count.Should().Be(1);
        cokes[0].Should().NotBeNull();
        cokes[0].CContent.Value.Should().Be(95);
        cokes[0].Proportion.Value.Should().Be(80);
    }

    [Fact]
    public void AddOneCokeAndNormalizeProportions()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(75.15));

        cokeBlend.Add(coke);
        cokeBlend.NormalizeProportions();
        cokeBlend.TotalProportion.Should().Be(100);

        var cokes = cokeBlend.Cokes;
        cokes.Should().NotBeNull();
        cokes.Count.Should().Be(1);
        cokes[0].Should().NotBeNull();
        cokes[0].CContent.Value.Should().Be(95);
        cokes[0].Proportion.Value.Should().Be(100);
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35));
        cokeBlend.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40));
        cokeBlend.Add(coke3);

        cokeBlend.ToString().Should().Be("{\r\n  \"Cokes\": [\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 95.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 25.0\r\n      }\r\n    },\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 85.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 35.0\r\n      }\r\n    },\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 80.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 40.0\r\n      }\r\n    }\r\n  ],\r\n  \"TotalProportion\": 100.0\r\n}");
    }
}
