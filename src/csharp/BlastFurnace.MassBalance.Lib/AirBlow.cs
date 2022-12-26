using Newtonsoft.Json;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describes composition and other variables related to air blow in Blast Furnace
/// </summary>
public class AirBlow
{
    /// <summary>
    /// Oxygen content in air blow
    /// </summary>
    public Percentual O2Content { get; set; }

    /// <summary>
    /// Initialize air blow properties
    /// </summary>
    /// <param name="o2Content"></param>
    public AirBlow(Percentual o2Content)
    {
        O2Content = o2Content;
    }

    /// <summary>
    /// String representation of air blow
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented);
        return jsonRepresentation.ToString();
    }
}
