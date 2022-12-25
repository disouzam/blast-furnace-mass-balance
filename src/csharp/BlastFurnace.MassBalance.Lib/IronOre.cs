using Newtonsoft.Json;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describes iron ore composition and its proportion in a mixture with other iron ores
/// </summary>
public class IronOre
{
    /// <summary>
    /// Fe content of Iron Ore
    /// </summary>
    public Percentual FeContent { get; set; }

    /// <summary>
    /// Proportion of Iron Ore in a blending setup
    /// </summary>
    public Percentual Proportion { get; set; }

    /// <summary>
    /// Initialize an iron ore instance
    /// </summary>
    /// <param name="feContent"></param>
    /// <param name="proportion"></param>
    public IronOre(Percentual feContent, Percentual proportion)
    {
        FeContent = feContent;
        Proportion = proportion;
    }

    /// <summary>
    /// String representation of IronOre
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented);
        return jsonRepresentation.ToString();
    }
}
