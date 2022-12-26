using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class CokeBlendTests
{
    [Fact]
    public void CreateNewObject()
    {
        var obj = new CokeBlend();
        obj.Should().NotBeNull();
    }

    [Fact]
    public void AddOneCokeToAnEmptyCokeBlend()
    {
        var obj = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(100));

        var act = () => obj.Add(coke);

        act.Should().NotThrow();
        obj.Should().NotBeNull();
        obj.TotalProportion.Should().Be(100);

        var readOnlyList = obj.Cokes;
        readOnlyList.Should().NotBeNull();
        readOnlyList.Count.Should().Be(1);
        readOnlyList[0].Should().NotBeNull();
        readOnlyList[0].CContent.Value.Should().Be(95);
        readOnlyList[0].Proportion.Value.Should().Be(100);
    }

    [Fact]
    public void AddCokesAndExceedHundredPercent()
    {
        var obj = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(80));
        obj.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(75));
        var act = () => obj.Add(coke2);

        act.Should().Throw<InvalidOperationException>().WithMessage("Total proportion must be at a maximum of 100%.");
        obj.Should().NotBeNull();
        obj.TotalProportion.Should().Be(80);

        var readOnlyList = obj.Cokes;
        readOnlyList.Should().NotBeNull();
        readOnlyList.Count.Should().Be(1);
        readOnlyList[0].Should().NotBeNull();
        readOnlyList[0].CContent.Value.Should().Be(95);
        readOnlyList[0].Proportion.Value.Should().Be(80);
    }

    [Fact]
    public void AddOneCokeAndNormalizeProportions()
    {
        var obj = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(75.15));

        obj.Add(coke);
        obj.NormalizeProportions();
        obj.TotalProportion.Should().Be(100);

        var readOnlyList = obj.Cokes;
        readOnlyList.Should().NotBeNull();
        readOnlyList.Count.Should().Be(1);
        readOnlyList[0].Should().NotBeNull();
        readOnlyList[0].CContent.Value.Should().Be(95);
        readOnlyList[0].Proportion.Value.Should().Be(100);
    }

    [Fact]
    public void CheckStringRepresentation()
    {
        var obj = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25));
        obj.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35));
        obj.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40));
        obj.Add(coke3);

        obj.ToString().Should().Be("{\r\n  \"Cokes\": [\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 95.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 25.0\r\n      }\r\n    },\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 85.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 35.0\r\n      }\r\n    },\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 80.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 40.0\r\n      }\r\n    }\r\n  ],\r\n  \"TotalProportion\": 100.0\r\n}");
    }
}
