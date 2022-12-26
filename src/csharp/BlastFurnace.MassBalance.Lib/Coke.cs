using Newtonsoft.Json;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describes coke composition and its proportion in a mixture with other cokes
/// </summary>
public class Coke
{
    /// <summary>
    /// C content of coke
    /// </summary>
    public Percentual CContent { get; set; }

    /// <summary>
    /// Proportion of coke in a blending setup
    /// </summary>
    public Percentual Proportion { get; set; }

    /// <summary>
    /// Initialize an coke instance
    /// </summary>
    /// <param name="cContent"></param>
    /// <param name="proportion"></param>
    public Coke(Percentual cContent, Percentual proportion)
    {
        CContent = cContent;
        Proportion = proportion;
    }

    /// <summary>
    /// String representation of Coke
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented);
        return jsonRepresentation.ToString();
    }
}
