using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// A collection of Cokes
/// </summary>
public class CokeBlend
{
    private readonly List<Coke> cokes = new List<Coke>();

    /// <summary>
    /// A read-only collection of Cokes
    /// </summary>
    public ReadOnlyCollection<Coke> Cokes
    {
        get
        {
            return cokes.AsReadOnly();
        }
    }

    /// <summary>
    /// Total proportion of Cokes in this blend.
    /// </summary>
    /// <remarks>
    /// For a full set up coke blend, this value must be equal to 100%
    /// For a partially defined coke blend, this value can be lower than 100%
    /// </remarks>
    public double TotalProportion { get; private set; }

    /// <summary>
    /// Add one coke to the blend of Cokes
    /// </summary>
    /// <param name="coke"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Add(Coke coke)
    {
        if (TotalProportion + coke.Proportion.Value > 100)
        {
            throw new InvalidOperationException("Total proportion must be at a maximum of 100%.");
        }

        TotalProportion += coke.Proportion.Value;
        cokes.Add(coke);
    }

    /// <summary>
    /// Normalize proportions of Cokes to 100%
    /// </summary>
    public void NormalizeProportions()
    {
        var tempTotalProportion = 0d;

        foreach (var coke in cokes)
        {
            coke.Proportion.Value = coke.Proportion.Value / TotalProportion * 100;
            tempTotalProportion += coke.Proportion.Value;
        }

        TotalProportion = tempTotalProportion;
    }

    /// <summary>
    /// String representation of CokeBlend
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented);

        return jsonRepresentation.ToString();
    }
}
