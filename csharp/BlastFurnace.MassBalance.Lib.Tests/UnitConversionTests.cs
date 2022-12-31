using System;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class UnitConversionTests
{
    [Theory]
    [InlineData(1, WeightUnits.kilogram, 0.001, WeightUnits.metricTon)]
    [InlineData(1, WeightUnits.kilogram, 1, WeightUnits.kilogram)]
    [InlineData(1, WeightUnits.metricTon, 1000, WeightUnits.kilogram)]
    [InlineData(1, WeightUnits.metricTon, 1, WeightUnits.metricTon)]
    public void CheckValidWeightConversions(double originalValue, WeightUnits originalWeightUnit, double convertedValue, WeightUnits newWeightUnit)
    {
        var originalWeight = new Weight(originalValue, originalWeightUnit);

        var convertedWeight = UnitConversion.WeightConversion(originalWeight, newWeightUnit);

        using (new AssertionScope())
        {
            convertedWeight.Value.Should().BeApproximately(convertedValue, 0.0001);
            convertedWeight.Unit.Should().Be(newWeightUnit);
        }
    }

    [Fact]
    public void CheckInvalidWeightConversions()
    {
        var originalWeight = new Weight(1, WeightUnits.kilogram);

        using (new AssertionScope())
        {
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

    [Theory]
    [InlineData(1, VolumeUnits.NL, 0.001, VolumeUnits.Nm3)]
    [InlineData(1, VolumeUnits.NL, 1, VolumeUnits.NL)]
    [InlineData(1, VolumeUnits.Nm3, 1000, VolumeUnits.NL)]
    [InlineData(1, VolumeUnits.Nm3, 1, VolumeUnits.Nm3)]
    public void CheckValidVolumeConversions(double originalValue, VolumeUnits originalVolumeUnit, double convertedValue, VolumeUnits newVolumeUnit)
    {
        var originalVolume = new Volume(originalValue, originalVolumeUnit);

        var convertedVolume = UnitConversion.VolumeConversion(originalVolume, newVolumeUnit);

        using (new AssertionScope())
        {
            convertedVolume.Value.Should().BeApproximately(convertedValue, 0.0001);
            convertedVolume.Unit.Should().Be(newVolumeUnit);
        }
    }

    [Fact]
    public void CheckInvalidVolumeConversions()
    {
        var originalVolume = new Volume(1, VolumeUnits.NL);

        using (new AssertionScope())
        {
            var act = () => UnitConversion.VolumeConversion(originalVolume, (VolumeUnits)int.MaxValue);
            act.Should().Throw<NotImplementedException>().WithMessage("Conversion between Nl and 2147483647 is not implemented yet!");

            var originalVolume2 = new Volume(1, VolumeUnits.Nm3);
            var act2 = () => UnitConversion.VolumeConversion(originalVolume2, (VolumeUnits)int.MaxValue);
            act2.Should().Throw<NotImplementedException>().WithMessage("Conversion between Nm3 and 2147483647 is not implemented yet!");

            var originalWeight3 = new Volume(1, (VolumeUnits)int.MaxValue);
            var act3 = () => UnitConversion.VolumeConversion(originalWeight3, VolumeUnits.Nm3);
            act3.Should().Throw<NotImplementedException>().WithMessage("Conversion between 2147483647 and Nm3 is not implemented yet!");
        }
    }
}
