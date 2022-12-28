using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// A collection of Iron Ores
/// </summary>
public class IronOreBlend
{
    private readonly List<IronOre> ironOres;

    private WeightUnits unit;

    /// <summary>
    /// Get the required weight of iron ore blend based on hot metal characteristics
    /// </summary>
    /// <returns></returns>
    public static Weight GetBlendRequiredWeight(HotMetal hotmetal)
    {
        var totalIronWeight = hotmetal.Weight.Value * hotmetal.FePercent.Value / 100;
        var response = new Weight(totalIronWeight, hotmetal.Weight.Unit);
        return response;
    }

    /// <summary>
    /// Initialization of iron ore blend
    /// </summary>
    public IronOreBlend()
    {
        ironOres = new List<IronOre>();
        TotalProportion = new Percentual(0);
    }

    /// <summary>
    /// A read-only collection of iron ores
    /// </summary>
    public ReadOnlyCollection<IronOre> IronOres
    {
        get
        {
            return ironOres.AsReadOnly();
        }
    }

    /// <summary>
    /// Total proportion of iron ores in this blend.
    /// </summary>
    /// <remarks>
    /// For a full set up iron ore blend, this value must be equal to 100%
    /// For a partially defined iron ore blend, this value can be lower than 100%
    /// </remarks>
    public Percentual TotalProportion { get; private set; }

    /// <summary>
    /// Calculate and returns average iron content of iron ore blend
    /// </summary>
    public Percentual AverageFeContent
    {
        get
        {
            var currentTotalProportion = 0.0d;
            if (TotalProportion.Value == 100)
            {
                currentTotalProportion = 100;
            }
            else
            {
                foreach (var ironOre in ironOres)
                {
                    currentTotalProportion += ironOre.Proportion.Value;
                }
            }

            var averageFeContentValue = 0.0d;

            foreach (var ironOre in ironOres)
            {
                averageFeContentValue += ironOre.FeContent.Value * ironOre.Proportion.Value / currentTotalProportion;
            }

            var averageFeContent = new Percentual(averageFeContentValue);

            return averageFeContent;
        }
    }

    /// <summary>
    /// Add one iron ore to the blend of iron ores
    /// </summary>
    /// <param name="ironOre"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Add(IronOre ironOre)
    {
        if (TotalProportion.Value + ironOre.Proportion.Value > 100)
        {
            throw new InvalidOperationException("Total proportion must be at a maximum of 100%. Iron ore passed as parameter was not added to the blend.");
        }

        if (ironOres.Count == 0)
        {
            unit = ironOre.Weight.Unit;
        }

        var convertedWeight = ironOre.Weight.GetWeightValue(unit);
        var newWeight = new Weight(convertedWeight, unit);
        var ironOreToAdd = new IronOre(ironOre.FeContent, ironOre.Proportion, newWeight);

        TotalProportion.Value += ironOreToAdd.Proportion.Value;
        ironOres.Add(ironOreToAdd);
    }

    /// <summary>
    /// Normalize proportions of iron ores to 100%
    /// </summary>
    public void NormalizeProportions()
    {
        var tempTotalProportion = 0d;

        foreach (var ironOre in ironOres)
        {
            ironOre.Proportion.Value = ironOre.Proportion.Value / TotalProportion.Value * 100;
            tempTotalProportion += ironOre.Proportion.Value;
        }

        TotalProportion.Value = tempTotalProportion;
    }

    /// <summary>
    /// String representation of IronOreBlend
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
