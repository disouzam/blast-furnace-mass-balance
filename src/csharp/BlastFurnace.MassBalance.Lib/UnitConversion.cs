using System;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Static class that performs conversion between different measurement units
/// </summary>
public static class UnitConversion
{
    private static readonly double metricTonTokgFactor = 1000;
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
}
