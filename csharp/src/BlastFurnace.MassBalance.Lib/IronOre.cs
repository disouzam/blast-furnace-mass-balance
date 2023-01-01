using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describes iron ore composition and its proportion in a mixture with other iron ores
/// </summary>
public class IronOre
{
    /// <summary>
    /// Fe content of Iron Ore
    /// </summary>
    public Percentual FeContent { get; private set; }

    /// <summary>
    /// Proportion of Iron Ore in a blending setup
    /// </summary>
    public Percentual Proportion { get; private set; }

    /// <summary>
    /// Initialize an iron ore instance with zero weight
    /// </summary>
    /// <param name="feContent"></param>
    /// <param name="proportion"></param>
    public IronOre(Percentual feContent, Percentual proportion)
    {
        FeContent = feContent;
        Proportion = proportion;
        Weight = new Weight(0, WeightUnits.metricTon);
    }

    /// <summary>
    /// Initialize an iron ore instance with a given proportion and weight
    /// </summary>
    /// <param name="feContent"></param>
    /// <param name="proportion"></param>
    /// <param name="weight"></param>
    public IronOre(Percentual feContent, Percentual proportion, Weight weight)
    {
        FeContent = feContent;
        Proportion = proportion;
        Weight = weight;
    }

    /// <summary>
    /// Weight of current iron ore
    /// </summary>
    public Weight Weight { get; private set; }

    /// <summary>
    /// Set new value and unit for iron ore weight
    /// </summary>
    /// <param name="newValue"></param>
    /// <param name="newUnit"></param>
    public void SetWeight(double newValue, WeightUnits newUnit)
    {
        var newWeight = new Weight(newValue, newUnit);
        Weight = newWeight;
    }

    /// <summary>
    /// String representation of IronOre
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
