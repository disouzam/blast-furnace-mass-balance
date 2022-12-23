using System;

namespace BlastFurnace.MassBalance.Lib;

public class Weight
{
    public double Value { get; private set; }

    public WeightUnits Unit { get; private set; }

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