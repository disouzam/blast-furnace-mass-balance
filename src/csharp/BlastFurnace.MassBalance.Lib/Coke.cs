using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        Weight = new Weight(0, WeightUnits.metricTon);
    }

    /// <summary>
    /// Initialize an coke instance
    /// </summary>
    /// <param name="cContent"></param>
    /// <param name="proportion"></param>
    /// <param name="weight"></param>
    public Coke(Percentual cContent, Percentual proportion, Weight weight)
    {
        CContent = cContent;
        Proportion = proportion;
        Weight = weight;
    }

    /// <summary>
    /// Weight of current coke
    /// </summary>
    public Weight Weight { get; private set; }

    /// <summary>
    /// Set new value and unit for coke weight
    /// </summary>
    /// <param name="newValue"></param>
    /// <param name="newUnit"></param>
    public void SetWeight(double newValue, WeightUnits newUnit)
    {
        var newWeight = new Weight(newValue, newUnit);
        Weight = newWeight;
    }

    /// <summary>
    /// String representation of Coke
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        // https://code-maze.com/csharp-serialize-enum-to-string/
        var serializerSettings = new JsonSerializerSettings();
        serializerSettings.Converters.Add(new StringEnumConverter());

        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);
        return jsonRepresentation.ToString();
    }
}
