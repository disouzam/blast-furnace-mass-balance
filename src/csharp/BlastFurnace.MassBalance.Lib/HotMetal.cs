using System;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describes hot metal weight and composition
/// </summary>
public class HotMetal
{
    /// <summary>
    /// Weight of hot metal
    /// </summary>
    public Weight Weight { get; set; }

    /// <summary>
    /// Percentage of Iron in Hot Metal
    /// </summary>
    public Percentual FePercent { get; set; }

    /// <summary>
    /// Percentage of Carbon in Hot Metal
    /// </summary>
    public Percentual CPercent { get; set; }

    /// <summary>
    /// Initialize and validate hot metal setting
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="fePercent"></param>
    /// <param name="cPercent"></param>
    public HotMetal(Weight weight, Percentual fePercent, Percentual cPercent)
    {
        if (fePercent.Value + cPercent.Value > 100)
        {
            throw new InvalidOperationException($"Composition should not exceed 100%! Current value is at {fePercent.Value + cPercent.Value} %.");
        }
        Weight = weight;
        FePercent = fePercent;
        CPercent = cPercent;
    }
}
