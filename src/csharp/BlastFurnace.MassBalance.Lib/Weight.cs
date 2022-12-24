using System;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Holds both the quantity and unit of weights processed by mass balance and other engineering calculations
/// </summary>
public class Weight
{
    /// <summary>
    /// The value of weight
    /// </summary>
    public double Value { get; private set; }

    /// <summary>
    /// The unit of measurement of weight
    /// </summary>
    public WeightUnits Unit { get; private set; }

    /// <summary>
    /// Initialize Weight and validate it
    /// </summary>
    /// <param name="value"></param>
    /// <param name="units"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Weight(double value, WeightUnits units)
    {
        if (value < 0 )
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} can't be negative!");
        }

        Value = value;
        Unit = units;
    }
}
