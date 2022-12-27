using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// A collection of Iron Ores
/// </summary>
public class IronOreBlend
{
    private readonly List<IronOre> ironOres = new List<IronOre>();

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
    public double TotalProportion { get; private set; }

    /// <summary>
    /// Calculate and returns average iron content of iron ore blend
    /// </summary>
    public Percentual AverageFeContent
    {
        get
        {
            var currentTotalProportion = 0.0d;
            if (TotalProportion == 100)
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
        if (TotalProportion + ironOre.Proportion.Value > 100)
        {
            throw new InvalidOperationException("Total proportion must be at a maximum of 100%.");
        }

        TotalProportion += ironOre.Proportion.Value;
        ironOres.Add(ironOre);
    }

    /// <summary>
    /// Normalize proportions of iron ores to 100%
    /// </summary>
    public void NormalizeProportions()
    {
        var tempTotalProportion = 0d;

        foreach (var ironOre in ironOres)
        {
            ironOre.Proportion.Value = ironOre.Proportion.Value / TotalProportion * 100;
            tempTotalProportion += ironOre.Proportion.Value;
        }

        TotalProportion = tempTotalProportion;
    }

    /// <summary>
    /// String representation of IronOreBlend
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented);

        return jsonRepresentation.ToString();
    }
}
