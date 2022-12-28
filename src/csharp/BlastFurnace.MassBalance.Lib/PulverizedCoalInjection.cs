using System;

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
    /// Initialize an Pulverized Coal Injection instance
    /// </summary>
    /// <param name="cContent"></param>
    public PulverizedCoalInjection(Percentual cContent)
    {
        CContent = cContent;
        Weight = new Weight(0, WeightUnits.metricTon);
    }

    /// <summary>
    /// Calculate the maximum PCI rate allowable
    /// </summary>
    /// <returns></returns>
    public double MaximumPCIRate(HotMetal hotMetal)
    {
        if (hotMetal == null)
        {
            throw new ArgumentNullException(nameof(hotMetal));
        }

        var hotMetalWeightinKg = hotMetal.Weight.GetWeightValue(WeightUnits.kilogram);
        var feWeightInHotMetalInKg = hotMetalWeightinKg * hotMetal.FePercent.Value / 100;

        // Mass of burned carbon (in kilograms)
        var PCqu = (0.02 * feWeightInHotMetalInKg) * (12);

        // Maximum weight of PCI allowed (also in kilograms)
        var PCI = PCqu / (CContent.Value / 100);

        // Maximum PCI rate in kilograms of PCI per metric ton of hot metal
        var PCIratmx = PCI / hotMetal.Weight.GetWeightValue(WeightUnits.metricTon);

        return PCIratmx;
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
