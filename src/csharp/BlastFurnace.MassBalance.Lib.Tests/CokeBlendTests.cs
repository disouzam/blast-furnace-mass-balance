using System;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class CokeBlendTests
{
    [Fact]
    public void CreateNewObject()
    {
        var cokeBlend = new CokeBlend();

        using (new AssertionScope())
        {
            cokeBlend.Should().NotBeNull("because no coke has been added.");
            cokeBlend.Cokes.Count.Should().Be(0, "because no coke has been added.");
            cokeBlend.AverageCContent.Value.Should().Be(0);
        }
    }

    [Fact]
    public void AddOneCokeToAnEmptyCokeBlend()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(100));

        var act = () => cokeBlend.Add(coke);

        using (new AssertionScope())
        {
            act.Should().NotThrow();
            cokeBlend.Should().NotBeNull();
            cokeBlend.TotalProportion.Value.Should().Be(100);
            cokeBlend.AverageCContent.Value.Should().Be(95);
        }

        var cokes = cokeBlend.Cokes;

        using (new AssertionScope())
        {
            cokes.Should().NotBeNull();
            cokes.Count.Should().Be(1);
            cokes[0].Should().NotBeNull();
            cokes[0].CContent.Value.Should().Be(95);
            cokes[0].Proportion.Value.Should().Be(100);
        }
    }

    [Fact]
    public void AddCokesAndExceedHundredPercent()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(80));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(75));
        var act = () => cokeBlend.Add(coke2);

        using (new AssertionScope())
        {
            act.Should().Throw<InvalidOperationException>().WithMessage("Total proportion must be at a maximum of 100%.");
            cokeBlend.Should().NotBeNull();
            cokeBlend.TotalProportion.Value.Should().Be(80);
            cokeBlend.AverageCContent.Value.Should().Be(95);
        }

        var cokes = cokeBlend.Cokes;

        using (new AssertionScope())
        {
            cokes.Should().NotBeNull();
            cokes.Count.Should().Be(1);
            cokes[0].Should().NotBeNull();
            cokes[0].CContent.Value.Should().Be(95);
            cokes[0].Proportion.Value.Should().Be(80);
        }
    }

    [Fact]
    public void AddOneCokeAndNormalizeProportions()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(75.15));

        cokeBlend.Add(coke);
        cokeBlend.NormalizeProportions();

        using (new AssertionScope())
        {
            cokeBlend.TotalProportion.Value.Should().Be(100);
            cokeBlend.AverageCContent.Value.Should().Be(95);
        }

        var cokes = cokeBlend.Cokes;

        using (new AssertionScope())
        {
            cokes.Should().NotBeNull();
            cokes.Count.Should().Be(1);
            cokes[0].Should().NotBeNull();
            cokes[0].CContent.Value.Should().Be(95);
            cokes[0].Proportion.Value.Should().Be(100);
        }
    }

    [Fact]
    public void AddCokesWithDifferentUnits()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25), new Weight(4500, WeightUnits.kilogram));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35), new Weight(9, WeightUnits.metricTon));
        cokeBlend.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40), new Weight(700, WeightUnits.kilogram));
        cokeBlend.Add(coke3);

        using (new AssertionScope())
        {
            var cokes = cokeBlend.Cokes;
            cokes.Count.Should().Be(3);
            cokes[0].Weight.Unit.Should().Be(WeightUnits.kilogram);
            cokes[1].Weight.Unit.Should().Be(WeightUnits.kilogram);
            cokes[2].Weight.Unit.Should().Be(WeightUnits.kilogram);
        }
    }

    [Fact]
    public void CheckMaximumCokeRate()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35));
        cokeBlend.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40));
        cokeBlend.Add(coke3);

        using (new AssertionScope())
        {
            var hotMetal = new HotMetal(new Weight(155, WeightUnits.metricTon), new Percentual(95), new Percentual(4));
            cokeBlend.MaximumCokeRate(hotMetal).Should().BeApproximately(551.545, 0.001);

            var hotMetal2 = new HotMetal(new Weight(155000, WeightUnits.kilogram), new Percentual(95), new Percentual(4));
            cokeBlend.MaximumCokeRate(hotMetal2).Should().BeApproximately(551.545, 0.001);
        }
    }

    [Fact]
    public void CheckMaximumCokeRateWithNullHotMetal()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35));
        cokeBlend.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40));
        cokeBlend.Add(coke3);

        HotMetal? hotmetal = null;
#pragma warning disable CS8604 // Possible null reference argument.
        var act = () => cokeBlend.MaximumCokeRate(hotmetal);
#pragma warning restore CS8604 // Possible null reference argument.
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CheckBlendRequiredWeight()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35));
        cokeBlend.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40));
        cokeBlend.Add(coke3);

        var hotMetal = new HotMetal(new Weight(155, WeightUnits.metricTon), new Percentual(95), new Percentual(4));        

        using (new AssertionScope())
        {
            var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(10, WeightUnits.metricTon));
            var blendRequiredWeight = cokeBlend.GetBlendRequiredWeight(hotMetal, pci);
            blendRequiredWeight.Value.Should().BeApproximately(74.963, 0.001);
            blendRequiredWeight.Unit.Should().Be(WeightUnits.metricTon);

            var pci2 = new PulverizedCoalInjection(new Percentual(90), new Weight(0, WeightUnits.metricTon));
            var blendRequiredWeight2 = cokeBlend.GetBlendRequiredWeight(hotMetal, pci2);
            blendRequiredWeight2.Value.Should().BeApproximately(85.489, 0.001);
            blendRequiredWeight2.Unit.Should().Be(WeightUnits.metricTon);

        }
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

        using (new AssertionScope())
        {
            cokeBlend.ToString().Should().Be("{\r\n  \"Cokes\": [\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 95.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 25.0\r\n      },\r\n      \"Weight\": {\r\n        \"Value\": 0.0,\r\n        \"Unit\": 1\r\n      }\r\n    },\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 85.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 35.0\r\n      },\r\n      \"Weight\": {\r\n        \"Value\": 0.0,\r\n        \"Unit\": 1\r\n      }\r\n    },\r\n    {\r\n      \"CContent\": {\r\n        \"Value\": 80.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 40.0\r\n      },\r\n      \"Weight\": {\r\n        \"Value\": 0.0,\r\n        \"Unit\": 1\r\n      }\r\n    }\r\n  ],\r\n  \"TotalProportion\": {\r\n    \"Value\": 100.0\r\n  },\r\n  \"AverageCContent\": {\r\n    \"Value\": 85.5\r\n  }\r\n}");
            cokeBlend.AverageCContent.Value.Should().Be(85.5);
        }
    }
}
