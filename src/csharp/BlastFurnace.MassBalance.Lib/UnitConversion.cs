using System;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Static class that performs conversion between different measurement units
/// </summary>
public static class UnitConversion
{
    private static readonly double metricTonTokgFactor = 1000;
    private static readonly double standardCubicMeterToLiterFactor = 1000;
    private static readonly string notImplementedUnitConversion = "Conversion between {0} and {1} is not implemented yet!";

    /// <summary>
    /// Performs conversion between weight units
    /// </summary>
    /// <param name="originalWeight"></param>
    /// <param name="newWeightUnits"></param>
    /// <returns></returns>
    public static Weight WeightConversion(Weight originalWeight, WeightUnits newWeightUnits)
    {
        double convertedValue;
        string formattedMessage;

        switch (originalWeight.Unit)
        {
            case WeightUnits.kilogram:
                switch (newWeightUnits)
                {
                    case WeightUnits.kilogram:
                        convertedValue = originalWeight.Value;
                        break;
                    case WeightUnits.metricTon:
                        convertedValue = originalWeight.Value / metricTonTokgFactor;
                        break;
                    default:
                        formattedMessage = String.Format(notImplementedUnitConversion, originalWeight.Unit, newWeightUnits);
                        throw new NotImplementedException(formattedMessage);
                }
                break;
            case WeightUnits.metricTon:
                switch (newWeightUnits)
                {
                    case WeightUnits.kilogram:
                        convertedValue = originalWeight.Value * metricTonTokgFactor;
                        break;
                    case WeightUnits.metricTon:
                        convertedValue = originalWeight.Value;
                        break;
                    default:
                        formattedMessage = String.Format(notImplementedUnitConversion, originalWeight.Unit, newWeightUnits);
                        throw new NotImplementedException(formattedMessage);
                }
                break;
            default:
                formattedMessage = String.Format(notImplementedUnitConversion, originalWeight.Unit, newWeightUnits);
                throw new NotImplementedException(formattedMessage);
        }

        var newWeight = new Weight(convertedValue, newWeightUnits);

        return newWeight;
    }

    /// <summary>
    /// Performs conversion between volume units
    /// </summary>
    /// <param name="originalVolume"></param>
    /// <param name="newVolumeUnits"></param>
    /// <returns></returns>
    public static Volume VolumeConversion(Volume originalVolume, VolumeUnits newVolumeUnits)
    {
        double convertedValue;
        string formattedMessage;

        switch (originalVolume.Unit)
        {
            case VolumeUnits.NL:
                switch (newVolumeUnits)
                {
                    case VolumeUnits.NL:
                        convertedValue = originalVolume.Value;
                        break;
                    case VolumeUnits.Nm3:
                        convertedValue = originalVolume.Value / standardCubicMeterToLiterFactor;
                        break;
                    default:
                        formattedMessage = string.Format(notImplementedUnitConversion, originalVolume.Unit, newVolumeUnits);
                        throw new NotImplementedException(formattedMessage);
                }
                break;
            case VolumeUnits.Nm3:
                switch (newVolumeUnits)
                {
                    case VolumeUnits.NL:
                        convertedValue = originalVolume.Value * standardCubicMeterToLiterFactor;
                        break;
                    case VolumeUnits.Nm3:
                        convertedValue = originalVolume.Value;
                        break;
                    default:
                        formattedMessage = string.Format(notImplementedUnitConversion, originalVolume.Unit, newVolumeUnits);
                        throw new NotImplementedException(formattedMessage);
                }
                break;
            default:
                formattedMessage = string.Format(notImplementedUnitConversion, originalVolume.Unit, newVolumeUnits);
                throw new NotImplementedException(formattedMessage);
        }

        var newVolume = new Volume(convertedValue, newVolumeUnits);

        return newVolume;
    }


    //VolumeConversion
}
