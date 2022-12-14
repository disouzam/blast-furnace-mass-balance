using System;

using Newtonsoft.Json;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describe percent quantities (from 0 to 100%) that describe raw material chemistry
/// </summary>
public class Percentual
{
    /// <summary>
    /// Hold the percentage value
    /// </summary>
    public double Value { get; private set; }

    /// <summary>
    /// Initialize and validate Percentual instance
    /// </summary>
    /// <param name="value"></param>
    public Percentual(double value)
    {
        SetValue(value);
    }

    /// <summary>
    /// Set value of the percentual
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(double value)
    {
        if (value < 0 || value > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Percentage value must belong to [0,100] interval. Current value ({value}) is invalid!");
        }
        Value = value;
    }

    /// <summary>
    /// String representation of Percentual
    /// </summary>
    public override string ToString()
    {
        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented);
        return jsonRepresentation.ToString();
    }
}
