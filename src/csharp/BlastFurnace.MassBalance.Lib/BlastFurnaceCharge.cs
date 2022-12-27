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
    public HotMetal? HotMetal { get; set; } = null;

    /// <summary>
    /// Iron ore blend used in blast furnace
    /// </summary>
    public IronOreBlend? IronOreBlend { get; set; } = null;

    /// <summary>
    ///  Coke blend used in blast furnace
    /// </summary>
    public CokeBlend? CokeBlend { get; set; } = null;

    /// <summary>
    /// Air composition to blow blast furnace
    /// </summary>
    public AirBlow? AirBlow { get; set; } = null;

    /// <summary>
    /// Pulverized coal injection composition and weight
    /// </summary>
    public PulverizedCoalInjection? PCI { get; set; } = null;
}
