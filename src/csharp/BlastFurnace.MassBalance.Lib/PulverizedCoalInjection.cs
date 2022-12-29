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
    public Percentual CContent { get; private set; }

    /// <summary>
    /// Weight of Pulverized Coal Injection
    /// </summary>
    public Weight Weight { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Initialize an Pulverized Coal Injection instance
    /// </summary>
    /// <param name="cContent"></param>
    /// <param name="weight"></param>
    public PulverizedCoalInjection(Percentual cContent, Weight weight)
    {
        CContent = cContent;
        SetWeight(weight);
    }

    /// <summary>
    /// Initialize an Pulverized Coal Injection instance
    /// </summary>
    /// <param name="cContent"></param>
    public PulverizedCoalInjection(Percentual cContent)
    {
        CContent = cContent;
        var weight = new Weight(0, WeightUnits.metricTon);
        SetWeight(weight);
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Set weight for PCI and validate the value against maximum PCI weight
    /// </summary>
    /// <param name="weight"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void SetWeight(Weight weight)
    {
        if (weight.GetWeightValue(WeightUnits.metricTon) > maximumPCIWeight.GetWeightValue(WeightUnits.metricTon))
        {
            throw new InvalidOperationException($"It is not possible to set weight of PCI higher than {maximumPCIWeight.GetWeightValue(WeightUnits.metricTon):F0} metric tons.");
        }
        this.Weight = new Weight(weight.Value, weight.Unit);
    }

    private Weight maximumPCIWeight = new Weight(double.MaxValue, WeightUnits.metricTon);

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

        var PCIMaxWeightInMetricTon = PCI / 1000;

        maximumPCIWeight = new Weight(PCIMaxWeightInMetricTon, WeightUnits.metricTon);

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
