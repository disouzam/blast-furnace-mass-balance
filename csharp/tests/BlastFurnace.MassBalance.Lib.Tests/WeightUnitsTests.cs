using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class WeightUnitsTests
{
    private readonly WeightUnits kilogram = WeightUnits.kilogram;
    private readonly WeightUnits metricTons = WeightUnits.metricTon;

    // Check number of items in this enum
    [Fact]
    public void CheckNumberOfWeightUnitsItems()
    {
        var item = WeightUnits.kilogram;
        var values = item.GetType().GetEnumValues();

        values.Length.Should().Be(2);
    }

    // Check each individual value - Integer representation
    [Fact] 
    public void CheckIntegerRepresentation() 
    {
        using (new AssertionScope())
        {
            ((int)kilogram).Should().Be(0);
            ((int)metricTons).Should().Be(1);
        }
    }

    // Check each individual value - String representation
    [Fact]
    public void CheckStringRepresentation()
    {
        using (new AssertionScope())
        {
            kilogram.ToString().Should().Be("kilogram");
            metricTons.ToString().Should().Be("metricTon");
        }
    }
}
