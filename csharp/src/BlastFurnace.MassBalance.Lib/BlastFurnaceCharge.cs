using System;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// A wrapper class to group all raw materials for a blast furnace and perform mass balance calculation
/// </summary>
public class BlastFurnaceCharge
{
    /// <summary>
    /// Add only Hot Metal to an existing instance of BlastFurnaceCharge
    /// </summary>
    /// <param name="hotMetal"></param>
    /// <returns>A handle of this instance after adding Hot Metal to the object</returns>
    public BlastFurnaceCharge AddHotMetal(HotMetal hotMetal)
    {
        if (HotMetal != null)
        {
            throw new InvalidOperationException("Hot Metal has been already initialized. You can edit it but not change the instance.");
        }
        HotMetal = hotMetal;
        return this;
    }

    /// <summary>
    /// Add only Iron Ore Blend to an existing instance of BlastFurnaceCharge
    /// </summary>
    /// <param name="ironOreBlend"></param>
    /// <returns>A handle of this instance after adding Iron Ore Blend to the object</returns>
    public BlastFurnaceCharge AddIronOreBlend(IronOreBlend ironOreBlend)
    {
        if (IronOreBlend != null)
        {
            throw new InvalidOperationException("Iron Ore Blend has been already initialized. You can edit it but not change the instance.");
        }
        IronOreBlend = ironOreBlend;
        return this;
    }

    /// <summary>
    /// Add only Coke Blend to an existing instance of BlastFurnaceCharge
    /// </summary>
    /// <param name="cokeBlend"></param>
    /// <returns>A handle of this instance after adding Coke Blend to the object</returns>
    public BlastFurnaceCharge AddCokeBlend(CokeBlend cokeBlend)
    {
        if (CokeBlend != null)
        {
            throw new InvalidOperationException("Coke Blend has been already initialized. You can edit it but not change the instance.");
        }
        CokeBlend = cokeBlend;
        return this;
    }

    /// <summary>
    /// Add only Pulverized Coal Injection to an existing instance of BlastFurnaceCharge
    /// </summary>
    /// <param name="pci"></param>
    /// <returns>A handle of this instance after adding Pulverized Coal Injection to the object</returns>
    public BlastFurnaceCharge AddPCI(PulverizedCoalInjection pci)
    {
        if (PCI != null)
        {
            throw new InvalidOperationException("Pulverized Coal Injection has been already initialized. You can edit it but not change the instance.");
        }
        PCI = pci;
        return this;
    }

    /// <summary>
    /// Add only Air Blow to an existing instance of BlastFurnaceCharge
    /// </summary>
    /// <param name="airBlow"></param>
    /// <returns>A handle of this instance after adding Air Blow to the object</returns>
    public BlastFurnaceCharge AddAirBlow(AirBlow airBlow)
    {
        if (AirBlow != null)
        {
            throw new InvalidOperationException("Air Blow has been already initialized. You can edit it but not change the instance.");
        }
        AirBlow = airBlow;
        return this;
    }

    /// <summary>
    /// Set iron ore weights based on hot metal setup
    /// </summary>
    public void SetIronOreWeights()
    {
        if (HotMetal == null)
        {
            throw new InvalidOperationException("Hot metal is not fully defined. Firstly set hot metal parameters before calculating iron ore weights.");
        }

        if (IronOreBlend == null)
        {
            throw new InvalidOperationException("Iron ore blend is not fully defined. Firstly set iron ore blend parameters before calculating iron ore weights.");
        }

        IronOreBlend.SetIronOreWeightsBasedOnRequiredWeight(HotMetal);
    }

    /// <summary>
    /// Get fuel rate (coke rate and PCI rate combined)
    /// </summary>
    /// <returns></returns>
    public double GetFuelRate()
    {
        if (CokeBlend == null)
        {
            throw new InvalidOperationException($"{nameof(CokeBlend)} is currently not configured properly. It is not possible to calculate fuel rate.");
        }

        if (PCI == null)
        {
            throw new InvalidOperationException($"{nameof(PCI)} is currently not configured properly. It is not possible to calculate fuel rate.");
        }

        if (HotMetal == null)
        {
            throw new InvalidOperationException($"{nameof(HotMetal)} is currently not configured properly. It is not possible to calculate fuel rate.");
        }

        return CokeBlend.CokeRate(HotMetal, PCI) + PCI.PCIRate(HotMetal);
    }

    /// <summary>
    /// Parameterless constructor
    /// </summary>
    public BlastFurnaceCharge()
    {
        // Intentionally empty
    }

    /// <summary>
    /// Initialization of Blast Furnace Charge
    /// </summary>
    /// <param name="hotMetal"></param>
    /// <param name="ironOreBlend"></param>
    /// <param name="cokeBlend"></param>
    /// <param name="pci"></param>
    /// <param name="airBlow"></param>
    public BlastFurnaceCharge(
        HotMetal hotMetal,
        IronOreBlend ironOreBlend,
        CokeBlend cokeBlend,
        PulverizedCoalInjection pci,
        AirBlow airBlow)
    {
        HotMetal = hotMetal;
        IronOreBlend = ironOreBlend;
        CokeBlend = cokeBlend;
        PCI = pci;
        AirBlow = airBlow;
    }

    /// <summary>
    /// Hot metal composition and weight that intends to be produced in blast furnace
    /// </summary>
    public HotMetal? HotMetal { get; private set; } = null;

    /// <summary>
    /// Iron ore blend used in blast furnace
    /// </summary>
    public IronOreBlend? IronOreBlend { get; private set; } = null;

    /// <summary>
    ///  Coke blend used in blast furnace
    /// </summary>
    public CokeBlend? CokeBlend { get; private set; } = null;

    /// <summary>
    /// Air composition to blow blast furnace
    /// </summary>
    public AirBlow? AirBlow { get; private set; } = null;

    /// <summary>
    /// Pulverized coal injection composition and weight
    /// </summary>
    public PulverizedCoalInjection? PCI { get; private set; } = null;
}
