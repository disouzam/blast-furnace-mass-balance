using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class IronOreBlendTests
{
    [Fact]
    public void CreateNewObject()
    {
        var obj = new IronOreBlend();
        obj.Should().NotBeNull();
    }

    [Fact]
    public void AddOneIronOreToAnEmptyIronOreBlend()
    {
        var obj = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(100));

        var act = () => obj.Add(ironOre);

        act.Should().NotThrow();
        obj.Should().NotBeNull();
    }

    [Fact]
    public void AddIronOresAndExceedHundredPercent()
    {
        var obj = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(80));
        obj.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(75));
        var act = () => obj.Add(ironOre2);

        act.Should().Throw<InvalidOperationException>().WithMessage("Total proportion must be at a maximum of 100%.");
        obj.Should().NotBeNull();
        obj.TotalProportion.Should().Be(80);
    }

    [Fact]
    public void AddOneIronOreAndNormalizeProportions()
    {
        var obj = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(75.15));

        obj.Add(ironOre);
        obj.NormalizeProportions();
        obj.TotalProportion.Should().Be(100);
    }
}
