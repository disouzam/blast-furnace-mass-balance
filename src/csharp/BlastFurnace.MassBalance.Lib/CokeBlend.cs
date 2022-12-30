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
    private readonly List<Coke> cokes;

    private WeightUnits unit;

    /// <summary>
    /// Initialization of coke blend
    /// </summary>
    public CokeBlend()
    {
        cokes = new List<Coke>();
        TotalProportion = new Percentual(0);
    }

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
    public Percentual TotalProportion { get; private set; }

    /// <summary>
    /// Calculate and returns average carbon content of coke blend
    /// </summary>
    public Percentual AverageCContent
    {
        get
        {
            var currentTotalProportion = 0.0d;
            if (TotalProportion.Value == 100)
            {
                currentTotalProportion = 100;
            }
            else
            {
                foreach (var coke in cokes)
                {
                    currentTotalProportion += coke.Proportion.Value;
                }
            }

            var averageCContentValue = 0.0d;

            foreach (var coke in cokes)
            {
                averageCContentValue += coke.CContent.Value * coke.Proportion.Value / currentTotalProportion;
            }

            var averageCContent = new Percentual(averageCContentValue);

            return averageCContent;
        }
    }

    /// <summary>
    /// Add one coke to the blend of Cokes
    /// </summary>
    /// <param name="coke"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Add(Coke coke)
    {
        if (TotalProportion.Value + coke.Proportion.Value > 100)
        {
            throw new InvalidOperationException("Total proportion must be at a maximum of 100%.");
        }

        if (cokes.Count == 0)
        {
            unit = coke.Weight.Unit;
        }

        var convertedWeight = coke.Weight.GetWeightValue(unit);
        var newWeight = new Weight(convertedWeight, unit);
        var cokeToAdd = new Coke(coke.CContent, coke.Proportion, newWeight);

        TotalProportion.SetValue(TotalProportion.Value + cokeToAdd.Proportion.Value);
        cokes.Add(cokeToAdd);
    }

    /// <summary>
    /// Normalize proportions of Cokes to 100%
    /// </summary>
    public void NormalizeProportions()
    {
        var tempTotalProportion = 0d;

        foreach (var coke in cokes)
        {
            coke.Proportion.SetValue(coke.Proportion.Value / TotalProportion.Value * 100);
            tempTotalProportion += coke.Proportion.Value;
        }

        TotalProportion.SetValue(tempTotalProportion);
    }

    /// <summary>
    /// Maximum coke rate in kg of coke / metric ton of hot metal
    /// </summary>
    /// <param name="hotMetal"></param>
    public double MaximumCokeRate(HotMetal hotMetal)
    {
        if (hotMetal == null)
        {
            throw new ArgumentNullException(nameof(hotMetal));
        }

        var atomicWeightCarbon = 12;
        var atomicWeightFe = 56;

        var hotMetalWeightinKg = hotMetal.Weight.GetWeightValue(WeightUnits.kilogram);

        var feWeightInHotMetalInKg = hotMetalWeightinKg * hotMetal.FePercent.Value / 100;
        var cWeightInHotMetalInKg = hotMetalWeightinKg * hotMetal.CPercent.Value / 100;

        // Number of kmols of carbon required to be loaded at blast furnace
        var numberOfKmolsCarbonRequired = (2.12 * feWeightInHotMetalInKg) / atomicWeightFe + cWeightInHotMetalInKg / atomicWeightCarbon;

        // Mass of Carbon that must be loaded at blast furnace
        var carbonWeightRequired = numberOfKmolsCarbonRequired * atomicWeightCarbon;

        // Coke weight without use of PCI
        var cokeWeightRequiredWithoutPCI = carbonWeightRequired / (AverageCContent.Value / 100);

        // Maximum coke rate
        var maxCokeRate = cokeWeightRequiredWithoutPCI / hotMetal.Weight.GetWeightValue(WeightUnits.metricTon);
        return maxCokeRate;
    }

    /// <summary>
    /// Minimum coke rate in kg of coke / metric ton of hot metal
    /// </summary>
    /// <param name="hotMetal"></param>
    public double MinimumCokeRate(HotMetal hotMetal)
    {
        var atomicWeightCarbon = 12;
        var atomicWeightFe = 56;

        var hotMetalWeightinKg = hotMetal.Weight.GetWeightValue(WeightUnits.kilogram);
        var hotMetalWeightinMetricTon = hotMetal.Weight.GetWeightValue(WeightUnits.metricTon);

        var feWeightInHotMetalInKg = hotMetalWeightinKg * hotMetal.FePercent.Value / 100;
        var cWeightInHotMetalInKg = hotMetalWeightinKg * hotMetal.CPercent.Value / 100;

        var minimumCokeRate = (((feWeightInHotMetalInKg / atomicWeightFe + (cWeightInHotMetalInKg) / (atomicWeightCarbon * 100)) * atomicWeightCarbon) * 100) / (hotMetalWeightinMetricTon * AverageCContent.Value);

        return minimumCokeRate;
    }

    /// <summary>
    /// Get the required weight of coke blend based on hot metal characteristics and pci characteristics
    /// </summary>
    /// <param name="hotMetal"></param>
    /// <param name="pci"></param>
    /// <returns></returns>
    public Weight GetBlendRequiredWeight(HotMetal hotMetal, PulverizedCoalInjection pci)
    {
        if (hotMetal == null)
        {
            throw new ArgumentNullException(nameof(hotMetal));
        }

        if (pci == null)
        {
            throw new ArgumentNullException(nameof(pci));
        }

        var hotMetalWeightUnitAdjusted = hotMetal.Weight.GetWeightValue(unit);
        var totalIronWeight = hotMetalWeightUnitAdjusted * hotMetal.FePercent.Value / 100;

        var pciAddedWeightUnitAdjusted = pci.Weight.GetWeightValue(unit);

        var blendRequiredWeight = (24 * totalIronWeight - pciAddedWeightUnitAdjusted * pci.CContent.Value + totalIronWeight * 150 / 7 + hotMetalWeightUnitAdjusted * hotMetal.CPercent.Value) / (AverageCContent.Value);

        var response = new Weight(blendRequiredWeight, unit);
        return response;
    }

    /// <summary>
    /// Set individual coke weights based on calculated required weight
    /// </summary>
    public void SetCokeWeightsBasedOnRequiredWeight(HotMetal hotMetal, PulverizedCoalInjection pci)
    {
        if (hotMetal == null)
        {
            throw new ArgumentNullException(nameof(hotMetal));
        }

        if (pci == null)
        {
            throw new ArgumentNullException(nameof(pci));
        }

        var totalWeight = GetBlendRequiredWeight(hotMetal, pci);

        var currentTotalProportion = 0.0d;
        if (TotalProportion.Value == 100)
        {
            currentTotalProportion = 100;
        }
        else
        {
            foreach (var coke in cokes)
            {
                currentTotalProportion += coke.Proportion.Value;
            }
        }

        foreach (var coke in cokes)
        {
            var currentCokeWeight = totalWeight.Value * coke.Proportion.Value / currentTotalProportion;
            coke.SetWeight(currentCokeWeight, unit);
        }
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
