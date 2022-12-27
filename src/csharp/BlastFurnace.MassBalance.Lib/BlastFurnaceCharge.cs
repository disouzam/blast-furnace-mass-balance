namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// A wrapper class to group all raw materials for a blast furnace and perform mass balance calculation
/// </summary>
public class BlastFurnaceCharge
{
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
        HotMetal= hotMetal;
        IronOreBlend= ironOreBlend;
        CokeBlend= cokeBlend;
        PCI= pci;
        AirBlow= airBlow;
    }

    /// <summary>
    /// Hot metal composition and weight that intends to be produced in blast furnace
    /// </summary>
    public HotMetal HotMetal { get; set; }

    /// <summary>
    /// Iron ore blend used in blast furnace
    /// </summary>
    public IronOreBlend IronOreBlend { get; set; }

    /// <summary>
    ///  Coke blend used in blast furnace
    /// </summary>
    public CokeBlend CokeBlend { get; set; }

    /// <summary>
    /// Air composition to blow blast furnace
    /// </summary>
    public AirBlow AirBlow { get; set; }

    /// <summary>
    /// Pulverized coal injection composition and weight
    /// </summary>
    public PulverizedCoalInjection PCI { get; set; }
}
