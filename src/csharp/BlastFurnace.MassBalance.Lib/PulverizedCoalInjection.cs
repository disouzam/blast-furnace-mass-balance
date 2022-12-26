using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describes Pulverized Coal Injection composition
/// </summary>
public class PulverizedCoalInjection
{
    /// <summary>
    /// C content of Pulverized Coal Injection
    /// </summary>
    public Percentual CContent { get; set; }

    /// <summary>
    /// Weight of Pulverized Coal Injection
    /// </summary>
    public Weight Weight { get; set; }

    /// <summary>
    /// Initialize an Pulverized Coal Injection instance
    /// </summary>
    /// <param name="cContent"></param>
    /// <param name="weight"></param>
    public PulverizedCoalInjection(Percentual cContent, Weight weight)
    {
        CContent = cContent;
        Weight = weight;
    }

    /// <summary>
    /// String representation of Pulverized Coal Injection
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
