using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class UnitConversionTests
{
    [Theory]
    [InlineData(1, WeightUnits.kilogram, 0.001, WeightUnits.metricTon)]
    [InlineData(1, WeightUnits.metricTon, 999, WeightUnits.kilogram)]
    public void CheckValidConversions(double originalValue, WeightUnits originalWeightUnit, double convertedValue, WeightUnits newWeightUnit)
    {
        var originalWeight = new Weight(originalValue, originalWeightUnit);

        var convertedWeight = UnitConversion.WeightConversion(originalWeight, newWeightUnit);

        convertedWeight.Value.Should().BeApproximately(convertedValue, 0.0001);
        convertedWeight.Unit.Should().Be(newWeightUnit);
    }
}
