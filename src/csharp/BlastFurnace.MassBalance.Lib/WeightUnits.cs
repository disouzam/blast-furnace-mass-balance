namespace BlastFurnace.MassBalance.Lib;


/// <summary>
/// Describe the weight units available to be used in mass balance and other engineering calculations
/// </summary>
/// <remarks>
/// <list type="bullet">
/// <item>kilogram</item>
/// <item>metric ton</item> 
/// </list>
/// </remarks>
public enum WeightUnits
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    kilogram = 0,

    metricTon = 1,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
