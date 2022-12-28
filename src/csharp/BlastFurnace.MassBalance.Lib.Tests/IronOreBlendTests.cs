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

        ironOreBlend.ToString().Should().Be("{\r\n  \"IronOres\": [\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 70.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 25.0\r\n      },\r\n      \"Weight\": {\r\n        \"Value\": 0.0,\r\n        \"Unit\": \"metricTon\"\r\n      }\r\n    },\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 65.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 35.0\r\n      },\r\n      \"Weight\": {\r\n        \"Value\": 0.0,\r\n        \"Unit\": \"metricTon\"\r\n      }\r\n    },\r\n    {\r\n      \"FeContent\": {\r\n        \"Value\": 60.0\r\n      },\r\n      \"Proportion\": {\r\n        \"Value\": 40.0\r\n      },\r\n      \"Weight\": {\r\n        \"Value\": 0.0,\r\n        \"Unit\": \"metricTon\"\r\n      }\r\n    }\r\n  ],\r\n  \"TotalProportion\": {\r\n    \"Value\": 100.0\r\n  },\r\n  \"AverageFeContent\": {\r\n    \"Value\": 64.25\r\n  }\r\n}");
        ironOreBlend.AverageFeContent.Value.Should().Be(64.25);
    }

    [Fact]
    public void CalculateRequiredWeightOfIronBlend()
    {
        var hotMetal = new HotMetal(new Weight(1000, WeightUnits.kilogram), new Percentual(94), new Percentual(5));

        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(25));
        ironOreBlend.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(35));
        ironOreBlend.Add(ironOre2);

        var ironOre3 = new IronOre(new Percentual(60), new Percentual(40));
        ironOreBlend.Add(ironOre3);

        ironOreBlend.GetBlendRequiredWeight(hotMetal).Value.Should().BeApproximately(1.463, 0.001);
        ironOreBlend.GetBlendRequiredWeight(hotMetal).Unit.Should().Be(WeightUnits.metricTon);
    }

    [Fact]
    public void CalculateAndSetRequiredWeightOfIronBlend()
    {
        var hotMetal = new HotMetal(new Weight(1000, WeightUnits.kilogram), new Percentual(94), new Percentual(5));

        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(25));
        ironOreBlend.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(35));
        ironOreBlend.Add(ironOre2);

        var ironOre3 = new IronOre(new Percentual(60), new Percentual(40));
        ironOreBlend.Add(ironOre3);

        using (new AssertionScope())
        {
            ironOreBlend.GetBlendRequiredWeight(hotMetal).Value.Should().BeApproximately(1.463, 0.001);
            ironOreBlend.GetBlendRequiredWeight(hotMetal).Unit.Should().Be(WeightUnits.metricTon);

            ironOreBlend.SetIronOreWeightsBasedOnRequiredWeight(hotMetal);

            var ironOres = ironOreBlend.IronOres;

            ironOres[0].FeContent.Value.Should().Be(70);
            ironOres[0].Proportion.Value.Should().Be(25);
            ironOres[0].Weight.Value.Should().BeApproximately(0.366, 0.001);
            ironOres[0].Weight.Unit.Should().Be(WeightUnits.metricTon);

            ironOres[1].FeContent.Value.Should().Be(65);
            ironOres[1].Proportion.Value.Should().Be(35);
            ironOres[1].Weight.Value.Should().BeApproximately(0.512, 0.001);
            ironOres[1].Weight.Unit.Should().Be(WeightUnits.metricTon);

            ironOres[2].FeContent.Value.Should().Be(60);
            ironOres[2].Proportion.Value.Should().Be(40);
            ironOres[2].Weight.Value.Should().BeApproximately(0.585, 0.001);
            ironOres[2].Weight.Unit.Should().Be(WeightUnits.metricTon);
        }

        var ironOreBlend2 = new IronOreBlend();
        var ironOre4 = new IronOre(new Percentual(70), new Percentual(15));
        ironOreBlend2.Add(ironOre4);

        var ironOre5 = new IronOre(new Percentual(65), new Percentual(45));
        ironOreBlend2.Add(ironOre5);

        var ironOre6 = new IronOre(new Percentual(60), new Percentual(25));
        ironOreBlend2.Add(ironOre6);

        using (new AssertionScope())
        {
            ironOreBlend2.GetBlendRequiredWeight(hotMetal).Value.Should().BeApproximately(1.459, 0.001);
            ironOreBlend2.GetBlendRequiredWeight(hotMetal).Unit.Should().Be(WeightUnits.metricTon);

            ironOreBlend2.SetIronOreWeightsBasedOnRequiredWeight(hotMetal);

            var ironOres = ironOreBlend2.IronOres;

            ironOres[0].FeContent.Value.Should().Be(70);
            ironOres[0].Proportion.Value.Should().Be(15);
            ironOres[0].Weight.Value.Should().BeApproximately(0.257, 0.001);
            ironOres[0].Weight.Unit.Should().Be(WeightUnits.metricTon);

            ironOres[1].FeContent.Value.Should().Be(65);
            ironOres[1].Proportion.Value.Should().Be(45);
            ironOres[1].Weight.Value.Should().BeApproximately(0.772, 0.001);
            ironOres[1].Weight.Unit.Should().Be(WeightUnits.metricTon);

            ironOres[2].FeContent.Value.Should().Be(60);
            ironOres[2].Proportion.Value.Should().Be(25);
            ironOres[2].Weight.Value.Should().BeApproximately(0.429, 0.001);
            ironOres[2].Weight.Unit.Should().Be(WeightUnits.metricTon);
        }
    }
}
