using System;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class BlastFurnaceChargeTests
{
    private readonly BlastFurnaceCharge blastFurnaceCharge0;

    private readonly BlastFurnaceCharge? blastFurnaceCharge1;

    private static HotMetal GetHotMetal()
    {
        var hotMetal = new HotMetal(new Weight(1, WeightUnits.kilogram), new Percentual(95), new Percentual(5));
        return hotMetal;
    }

    private static IronOreBlend GetIronOreBlend()
    {
        var ironOreBlend = new IronOreBlend();
        var ironOre = new IronOre(new Percentual(70), new Percentual(25));
        ironOreBlend.Add(ironOre);

        var ironOre2 = new IronOre(new Percentual(65), new Percentual(35));
        ironOreBlend.Add(ironOre2);

        var ironOre3 = new IronOre(new Percentual(60), new Percentual(40));
        ironOreBlend.Add(ironOre3);

        return ironOreBlend;
    }

    private static CokeBlend GetCokeBlend()
    {
        var cokeBlend = new CokeBlend();
        var coke = new Coke(new Percentual(95), new Percentual(25));
        cokeBlend.Add(coke);

        var coke2 = new Coke(new Percentual(85), new Percentual(35));
        cokeBlend.Add(coke2);

        var coke3 = new Coke(new Percentual(80), new Percentual(40));
        cokeBlend.Add(coke3);

        return cokeBlend;
    }

    private static PulverizedCoalInjection GetPCI()
    {
        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(100, WeightUnits.metricTon));
        return pci;
    }

    private static PulverizedCoalInjection GetPCI2()
    {
        var pci = new PulverizedCoalInjection(new Percentual(90), new Weight(0.5, WeightUnits.kilogram));
        return pci;
    }

    private static AirBlow GetAirBlow()
    {
        var airBlow = new AirBlow(new Percentual(21));
        return airBlow;
    }

    private static BlastFurnaceCharge GetBlastFurnaceChargeInstance()
    {
        var hotMetal = GetHotMetal();
        var ironOreBlend = GetIronOreBlend();
        var cokeBlend = GetCokeBlend();
        var pci = GetPCI();
        var airBlow = GetAirBlow();

        var blastFurnaceCharge = new BlastFurnaceCharge(hotMetal, ironOreBlend, cokeBlend, pci, airBlow);

        return blastFurnaceCharge;
    }

    private static BlastFurnaceCharge GetRealisticallyBlastFurnaceChargeInstance()
    {
        var hotMetal = GetHotMetal();
        var ironOreBlend = GetIronOreBlend();
        var cokeBlend = GetCokeBlend();
        var pci = GetPCI2();
        var airBlow = GetAirBlow();

        var blastFurnaceCharge = new BlastFurnaceCharge(hotMetal, ironOreBlend, cokeBlend, pci, airBlow);

        return blastFurnaceCharge;
    }

    private static BlastFurnaceCharge GetEmptyBlastFurnaceChargeInstance()
    {
        var blastFurnaceCharge = new BlastFurnaceCharge();
        return blastFurnaceCharge;
    }

    public BlastFurnaceChargeTests()
    {
        blastFurnaceCharge0 = GetBlastFurnaceChargeInstance();
        blastFurnaceCharge1 = GetEmptyBlastFurnaceChargeInstance();
    }

    [Fact]
    public void CheckInitialization()
    {
        using (new AssertionScope())
        {
            blastFurnaceCharge0.Should().NotBeNull();
            blastFurnaceCharge0.Should().BeOfType<BlastFurnaceCharge>();

            blastFurnaceCharge0.HotMetal.Should().NotBeNull();
            blastFurnaceCharge0.HotMetal.Should().BeOfType<HotMetal>();
            blastFurnaceCharge0.HotMetal?.Weight.Value.Should().Be(1);
            blastFurnaceCharge0.HotMetal?.Weight.Unit.Should().Be(WeightUnits.kilogram);

            blastFurnaceCharge0.IronOreBlend.Should().NotBeNull();
            blastFurnaceCharge0.IronOreBlend.Should().BeOfType<IronOreBlend>();
            blastFurnaceCharge0.IronOreBlend?.TotalProportion.Value.Should().Be(100);
            blastFurnaceCharge0.IronOreBlend?.AverageFeContent.Value.Should().Be(64.25);
            blastFurnaceCharge0.IronOreBlend?.IronOres.Count.Should().Be(3);

            blastFurnaceCharge0.CokeBlend.Should().NotBeNull();
            blastFurnaceCharge0.CokeBlend.Should().BeOfType<CokeBlend>();
            blastFurnaceCharge0.CokeBlend?.TotalProportion.Value.Should().Be(100);
            blastFurnaceCharge0.CokeBlend?.AverageCContent.Value.Should().Be(85.5);
            blastFurnaceCharge0.CokeBlend?.Cokes.Count.Should().Be(3);

            blastFurnaceCharge0.PCI.Should().NotBeNull();
            blastFurnaceCharge0.PCI.Should().BeOfType<PulverizedCoalInjection>();
            blastFurnaceCharge0.PCI?.CContent.Value.Should().Be(90);
            blastFurnaceCharge0.PCI?.Weight.Value.Should().Be(100);
            blastFurnaceCharge0.PCI?.Weight.Unit.Should().Be(WeightUnits.metricTon);

            blastFurnaceCharge0.AirBlow.Should().NotBeNull();
            blastFurnaceCharge0.AirBlow.Should().BeOfType<AirBlow>();
            blastFurnaceCharge0.AirBlow?.O2Content.Value.Should().Be(21);
        }

        using (new AssertionScope())
        {
            blastFurnaceCharge1.Should().NotBeNull();
            blastFurnaceCharge1.Should().BeOfType<BlastFurnaceCharge>();

            blastFurnaceCharge1?.HotMetal.Should().BeNull();
            blastFurnaceCharge1?.IronOreBlend.Should().BeNull();
            blastFurnaceCharge1?.CokeBlend.Should().BeNull();
            blastFurnaceCharge1?.PCI.Should().BeNull();
            blastFurnaceCharge1?.AirBlow.Should().BeNull();
        }
    }

    [Fact]
    public void CheckFuelRate()
    {
        var blastFurnaceCharge = GetRealisticallyBlastFurnaceChargeInstance();
        blastFurnaceCharge.GetFuelRate().Should().BeApproximately(536.925, 0.001);
    }

    [Fact]
    public void CheckFuelRateWithNullEntries()
    {
        var blastFurnaceCharge = new BlastFurnaceCharge();

        using (new AssertionScope())
        {
            var act = () => blastFurnaceCharge.GetFuelRate();
            act.Should().Throw<InvalidOperationException>().WithMessage("CokeBlend is currently not configured properly. It is not possible to calculate fuel rate.");

            var hotMetal = GetHotMetal();
            var cokeBlend = GetCokeBlend();
            var pci = GetPCI2();

            blastFurnaceCharge.AddCokeBlend(cokeBlend);
            act.Should().Throw<InvalidOperationException>().WithMessage("PCI is currently not configured properly. It is not possible to calculate fuel rate.");

            blastFurnaceCharge.AddPCI(pci);
            act.Should().Throw<InvalidOperationException>().WithMessage("HotMetal is currently not configured properly. It is not possible to calculate fuel rate.");

            blastFurnaceCharge.AddHotMetal(hotMetal);
            act.Should().NotThrow();
        }
    }

    [Fact]
    public void CheckAddHotMetal()
    {
        var hotMetal = GetHotMetal();
        blastFurnaceCharge1?.AddHotMetal(hotMetal);

        using (new AssertionScope())
        {
            blastFurnaceCharge1?.HotMetal.Should().NotBeNull();
            blastFurnaceCharge1?.IronOreBlend.Should().BeNull();
            blastFurnaceCharge1?.CokeBlend.Should().BeNull();
            blastFurnaceCharge1?.PCI.Should().BeNull();
            blastFurnaceCharge1?.AirBlow.Should().BeNull();

            blastFurnaceCharge1?.HotMetal.Should().NotBeNull();
            blastFurnaceCharge1?.HotMetal.Should().BeOfType<HotMetal>();
            blastFurnaceCharge1?.HotMetal?.Weight.Value.Should().Be(1);
            blastFurnaceCharge1?.HotMetal?.Weight.Unit.Should().Be(WeightUnits.kilogram);
        }
    }

    [Fact]
    public void CheckAddIronOreBlend()
    {
        var ironOreBlend = GetIronOreBlend();

        blastFurnaceCharge1?.AddIronOreBlend(ironOreBlend);

        using (new AssertionScope())
        {
            blastFurnaceCharge1?.HotMetal.Should().BeNull();
            blastFurnaceCharge1?.IronOreBlend.Should().NotBeNull();
            blastFurnaceCharge1?.CokeBlend.Should().BeNull();
            blastFurnaceCharge1?.PCI.Should().BeNull();
            blastFurnaceCharge1?.AirBlow.Should().BeNull();

            blastFurnaceCharge1?.IronOreBlend.Should().NotBeNull();
            blastFurnaceCharge1?.IronOreBlend.Should().BeOfType<IronOreBlend>();
            blastFurnaceCharge1?.IronOreBlend?.TotalProportion.Value.Should().Be(100);
            blastFurnaceCharge1?.IronOreBlend?.AverageFeContent.Value.Should().Be(64.25);
            blastFurnaceCharge1?.IronOreBlend?.IronOres.Count.Should().Be(3);
        }
    }

    [Fact]
    public void CheckAddCokeBlend()
    {
        var cokeBlend = GetCokeBlend();

        blastFurnaceCharge1?.AddCokeBlend(cokeBlend);

        using (new AssertionScope())
        {
            blastFurnaceCharge1?.HotMetal.Should().BeNull();
            blastFurnaceCharge1?.IronOreBlend.Should().BeNull();
            blastFurnaceCharge1?.CokeBlend.Should().NotBeNull();
            blastFurnaceCharge1?.PCI.Should().BeNull();
            blastFurnaceCharge1?.AirBlow.Should().BeNull();

            blastFurnaceCharge1?.CokeBlend.Should().NotBeNull();
            blastFurnaceCharge1?.CokeBlend.Should().BeOfType<CokeBlend>();
            blastFurnaceCharge1?.CokeBlend?.TotalProportion.Value.Should().Be(100);
            blastFurnaceCharge1?.CokeBlend?.AverageCContent.Value.Should().Be(85.5);
            blastFurnaceCharge1?.CokeBlend?.Cokes.Count.Should().Be(3);
        }
    }

    [Fact]
    public void CheckAddPCI()
    {
        var pci = GetPCI();
        blastFurnaceCharge1?.AddPCI(pci);

        using (new AssertionScope())
        {
            blastFurnaceCharge1?.HotMetal.Should().BeNull();
            blastFurnaceCharge1?.IronOreBlend.Should().BeNull();
            blastFurnaceCharge1?.CokeBlend.Should().BeNull();
            blastFurnaceCharge1?.PCI.Should().NotBeNull();
            blastFurnaceCharge1?.AirBlow.Should().BeNull();

            blastFurnaceCharge1?.PCI.Should().NotBeNull();
            blastFurnaceCharge1?.PCI.Should().BeOfType<PulverizedCoalInjection>();
            blastFurnaceCharge1?.PCI?.CContent.Value.Should().Be(90);
            blastFurnaceCharge1?.PCI?.Weight.Value.Should().Be(100);
            blastFurnaceCharge1?.PCI?.Weight.Unit.Should().Be(WeightUnits.metricTon);
        }
    }

    [Fact]
    public void CheckAddAirBlow()
    {
        var airBlow = GetAirBlow();
        blastFurnaceCharge1?.AddAirBlow(airBlow);

        using (new AssertionScope())
        {
            blastFurnaceCharge1?.HotMetal.Should().BeNull();
            blastFurnaceCharge1?.IronOreBlend.Should().BeNull();
            blastFurnaceCharge1?.CokeBlend.Should().BeNull();
            blastFurnaceCharge1?.PCI.Should().BeNull();
            blastFurnaceCharge1?.AirBlow.Should().NotBeNull();

            blastFurnaceCharge1?.AirBlow.Should().NotBeNull();
            blastFurnaceCharge1?.AirBlow.Should().BeOfType<AirBlow>();
            blastFurnaceCharge1?.AirBlow?.O2Content.Value.Should().Be(21);
        }
    }

    [Fact]
    public void TryAddingAnotherHotMetal()
    {
        var hotMetal = GetHotMetal();
        blastFurnaceCharge1?.AddHotMetal(hotMetal);

        var act = () => blastFurnaceCharge1?.AddHotMetal(hotMetal);

        using (new AssertionScope())
        {
            act.Should().Throw<InvalidOperationException>().WithMessage("Hot Metal has been already initialized. You can edit it but not change the instance.");
        }
    }

    [Fact]
    public void TryAddingAnotherIronOreBlend()
    {
        var ironOreBlend = GetIronOreBlend();
        blastFurnaceCharge1?.AddIronOreBlend(ironOreBlend);

        var act = () => blastFurnaceCharge1?.AddIronOreBlend(ironOreBlend);

        using (new AssertionScope())
        {
            act.Should().Throw<InvalidOperationException>().WithMessage("Iron Ore Blend has been already initialized. You can edit it but not change the instance.");
        }
    }

    [Fact]
    public void TryAddingAnotherCokeBlend()
    {
        var cokeBlend = GetCokeBlend();
        blastFurnaceCharge1?.AddCokeBlend(cokeBlend);

        var act = () => blastFurnaceCharge1?.AddCokeBlend(cokeBlend);

        using (new AssertionScope())
        {
            act.Should().Throw<InvalidOperationException>().WithMessage("Coke Blend has been already initialized. You can edit it but not change the instance.");
        }
    }

    [Fact]
    public void TryAddingAnotherPCI()
    {
        var pci = GetPCI();
        blastFurnaceCharge1?.AddPCI(pci);

        var act = () => blastFurnaceCharge1?.AddPCI(pci);

        using (new AssertionScope())
        {
            act.Should().Throw<InvalidOperationException>().WithMessage("Pulverized Coal Injection has been already initialized. You can edit it but not change the instance.");
        }
    }

    [Fact]
    public void TryAddingAnotherAirBlow()
    {
        var airBlow = GetAirBlow();
        blastFurnaceCharge1?.AddAirBlow(airBlow);

        var act = () => blastFurnaceCharge1?.AddAirBlow(airBlow);

        using (new AssertionScope())
        {
            act.Should().Throw<InvalidOperationException>().WithMessage("Air Blow has been already initialized. You can edit it but not change the instance.");
        }
    }

    [Fact]
    public void TrySetIronOreWeightsWithoutHotMetal()
    {
        var blastFurnaceCharge = new BlastFurnaceCharge();

        var act = () => blastFurnaceCharge.SetIronOreWeights();

        act.Should().Throw<InvalidOperationException>().WithMessage("Hot metal is not fully defined. Firstly set hot metal parameters before calculating iron ore weights.");
    }

    [Fact]
    public void TrySetIronOreWeightsWithoutIronOreBlend()
    {
        var blastFurnaceCharge = new BlastFurnaceCharge();

        var hotMetal = GetHotMetal();
        blastFurnaceCharge?.AddHotMetal(hotMetal);

        var act = () => blastFurnaceCharge?.SetIronOreWeights();

        act.Should().Throw<InvalidOperationException>().WithMessage("Iron ore blend is not fully defined. Firstly set iron ore blend parameters before calculating iron ore weights.");
    }

    [Fact]
    public void SetIronOreWeights()
    {
        var blastFurnaceCharge = new BlastFurnaceCharge();

        var hotMetal = new HotMetal(new Weight(1, WeightUnits.metricTon), new Percentual(95), new Percentual(5));
        blastFurnaceCharge?.AddHotMetal(hotMetal);

        var ironOreBlend = GetIronOreBlend();
        blastFurnaceCharge?.AddIronOreBlend(ironOreBlend);


        using (new AssertionScope())
        {
            var ironOres = blastFurnaceCharge?.IronOreBlend?.IronOres;

            ironOres?[0].Weight.Value.Should().BeApproximately(0, 0.001);
            ironOres?[1].Weight.Value.Should().BeApproximately(0, 0.001);
            ironOres?[2].Weight.Value.Should().BeApproximately(0, 0.001);

            blastFurnaceCharge?.SetIronOreWeights();

            ironOres = blastFurnaceCharge?.IronOreBlend?.IronOres;
            ironOres?[0].Weight.Value.Should().BeApproximately(0.369, 0.001);
            ironOres?[1].Weight.Value.Should().BeApproximately(0.517, 0.001);
            ironOres?[2].Weight.Value.Should().BeApproximately(0.591, 0.001);
        }
    }
}
