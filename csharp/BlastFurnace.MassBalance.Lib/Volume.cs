using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Holds both the quantity and unit of volume processed by mass balance and other engineering calculations
/// </summary>
public class Volume
{
    /// <summary>
    /// The value of volume
    /// </summary>
    public double Value { get; private set; }

    /// <summary>
    /// The unit of measurement of volume
    /// </summary>
    public VolumeUnits Unit { get; private set; }

    /// <summary>
    /// Get double converted value associated with this volume in the unit specified in outputUnit
    /// </summary>
    /// <param name="outputUnit"></param>
    /// <returns></returns>
    public double GetVolumeValue(VolumeUnits outputUnit)
    {
        return UnitConversion.VolumeConversion(this,outputUnit).Value;
    }

    /// <summary>
    /// Initialize Volume and validate it
    /// </summary>
    /// <param name="value"></param>
    /// <param name="units"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Volume(double value, VolumeUnits units)
    {
        if (value < 0 )
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} can't be negative!");
        }

        Value = value;
        Unit = units;
    }

    /// <summary>
    /// String representation
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
