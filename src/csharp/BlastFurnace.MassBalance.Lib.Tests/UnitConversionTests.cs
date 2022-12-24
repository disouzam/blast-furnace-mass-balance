using System;

using FluentAssertions;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class UnitConversionTests
{
    [Theory]
    [InlineData(1, WeightUnits.kilogram, 0.001, WeightUnits.metricTon)]
    [InlineData(1, WeightUnits.kilogram, 1, WeightUnits.kilogram)]
    [InlineData(1, WeightUnits.metricTon, 1000, WeightUnits.kilogram)]
    [InlineData(1, WeightUnits.metricTon, 1, WeightUnits.metricTon)]
    public void CheckValidConversions(double originalValue, WeightUnits originalWeightUnit, double convertedValue, WeightUnits newWeightUnit)
    {
        var originalWeight = new Weight(originalValue, originalWeightUnit);

        var convertedWeight = UnitConversion.WeightConversion(originalWeight, newWeightUnit);

        convertedWeight.Value.Should().BeApproximately(convertedValue, 0.0001);
        convertedWeight.Unit.Should().Be(newWeightUnit);
    }

    [Fact]
    public void CheckInvalidWeightConversions()
    {
        var originalWeight = new Weight(1, WeightUnits.kilogram);
        var act = () => UnitConversion.WeightConversion(originalWeight, (WeightUnits)int.MaxValue);
        act.Should().Throw<NotImplementedException>().WithMessage("Conversion between kilogram and 2147483647 is not implemented yet!");

        var originalWeight2 = new Weight(1, WeightUnits.metricTon);
        var act2 = () => UnitConversion.WeightConversion(originalWeight2, (WeightUnits)int.MaxValue);
        act2.Should().Throw<NotImplementedException>().WithMessage("Conversion between metricTon and 2147483647 is not implemented yet!");

        var originalWeight3 = new Weight(1, (WeightUnits)int.MaxValue);
        var act3 = () => UnitConversion.WeightConversion(originalWeight3, WeightUnits.kilogram);
        act3.Should().Throw<NotImplementedException>().WithMessage("Conversion between 2147483647 and kilogram is not implemented yet!");
    }
}
