using System;
using System.Runtime.ConstrainedExecution;

using Newtonsoft.Json;

namespace BlastFurnace.MassBalance.Lib;

/// <summary>
/// Describes composition and other variables related to air blow in Blast Furnace
/// </summary>
public class AirBlow
{
    /// <summary>
    /// Oxygen content in air blow
    /// </summary>
    public Percentual O2Content { get; private set; }

    /// <summary>
    /// Initialize air blow properties
    /// </summary>
    /// <param name="o2Content"></param>
    public AirBlow(Percentual o2Content)
    {
        O2Content = o2Content;
    }

    //{ Cálculo da vazão de sopro(em Nm3 / min)}
    //Vazao:= Vsopro / 1440;

    /// <summary>
    /// Get required air volume
    /// </summary>
    /// <param name="hotMetal"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Volume GetRequiredAirVolume(HotMetal hotMetal)
    {
        if (hotMetal == null)
        {
            throw new ArgumentNullException(nameof(hotMetal));
        }

        var hotMetalWeightinKg = hotMetal.Weight.GetWeightValue(WeightUnits.kilogram);
        var feWeightInHotMetalInKg = hotMetalWeightinKg * hotMetal.FePercent.Value / 100;

        var requiredOxygenVolume = 0.224 * feWeightInHotMetalInKg;

        var requiredAirVolumeValue = requiredOxygenVolume / (O2Content.Value / 100);

        var requiredAirVolume = new Volume(requiredAirVolumeValue, VolumeUnits.Nm3);
        return requiredAirVolume;
    }

    /// <summary>
    /// Air blow rate in Nm3/min
    /// </summary>
    /// <returns></returns>
    public double AirBlowRateRequiredPerMinute(HotMetal hotMetal)
    {
        if (hotMetal == null)
        {
            throw new ArgumentNullException(nameof(hotMetal));
        }

        var blowRate = GetRequiredAirVolume(hotMetal).GetVolumeValue(VolumeUnits.Nm3) / 1440;

        return blowRate;
    }

    /// <summary>
    /// String representation of air blow
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var jsonRepresentation = JsonConvert.SerializeObject(this, Formatting.Indented);
        return jsonRepresentation.ToString();
    }
}
