using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class BlastFurnaceChargeTests
{
    private readonly BlastFurnaceCharge blastFurnaceCharge0;

    private static BlastFurnaceCharge GetBlastFurnaceChargeInstance()
    {
        var hotMetal = new HotMetal(new Weight(1, WeightUnits.kilogram), new Percentual(95), new Percentual(5));

        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(25));
        ironOreBlend.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(35));
        ironOreBlend.Add(ironOre2);

        var ironOre3 = new IronOre(new Percentual(60), new Percentual(40));
        ironOreBlend.Add(ironOre3);

        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35));
        cokeBlend.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40));
        cokeBlend.Add(coke3);

        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(100, WeightUnits.metricTon));

        var airBlow = new AirBlow(new Percentual(21));

        var blastFurnaceCharge = new BlastFurnaceCharge(hotMetal, ironOreBlend, cokeBlend, pci, airBlow);

        return blastFurnaceCharge;
    }

    public BlastFurnaceChargeTests()
    {
        blastFurnaceCharge0 = GetBlastFurnaceChargeInstance();
    }

    [Fact]
    public void CheckInitialization()
    {
        using (new AssertionScope())
        {
            blastFurnaceCharge0.HotMetal.Should().NotBeNull();
            blastFurnaceCharge0.HotMetal.Should().BeOfType<HotMetal>();
            blastFurnaceCharge0.HotMetal.Weight.Value.Should().Be(1);
            blastFurnaceCharge0.HotMetal.Weight.Unit.Should().Be(WeightUnits.kilogram);

            blastFurnaceCharge0.IronOreBlend.Should().NotBeNull();
            blastFurnaceCharge0.IronOreBlend.Should().BeOfType<IronOreBlend>();
            blastFurnaceCharge0.IronOreBlend.TotalProportion.Value.Should().Be(100);
            blastFurnaceCharge0.IronOreBlend.AverageFeContent.Value.Should().Be(64.25);
            blastFurnaceCharge0.IronOreBlend.IronOres.Count.Should().Be(3);

            blastFurnaceCharge0.CokeBlend.Should().NotBeNull();
            blastFurnaceCharge0.CokeBlend.Should().BeOfType<CokeBlend>();
            blastFurnaceCharge0.CokeBlend.TotalProportion.Value.Should().Be(100);
            blastFurnaceCharge0.CokeBlend.AverageCContent.Value.Should().Be(85.5);
            blastFurnaceCharge0.CokeBlend.Cokes.Count.Should().Be(3);

            blastFurnaceCharge0.PCI.Should().NotBeNull();
            blastFurnaceCharge0.PCI.Should().BeOfType<PulverizedCoalInjection>();
            blastFurnaceCharge0.PCI.CContent.Value.Should().Be(90);
            blastFurnaceCharge0.PCI.Weight.Value.Should().Be(100);
            blastFurnaceCharge0.PCI.Weight.Unit.Should().Be(WeightUnits.metricTon);

            blastFurnaceCharge0.AirBlow.Should().NotBeNull();
            blastFurnaceCharge0.AirBlow.Should().BeOfType<AirBlow>();
            blastFurnaceCharge0.AirBlow.O2Content.Value.Should().Be(21);
        }
    }
}
