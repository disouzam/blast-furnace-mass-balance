using System;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace BlastFurnace.MassBalance.Lib.Tests;

public class VolumeTests
{
    [Fact]
    public void ValidVolumeInNm3()
    {
        var newVolume = new Volume(1000, VolumeUnits.Nm3);

        using (new AssertionScope())
        {
            newVolume.Value.Should().Be(1000);
            newVolume.Unit.Should().Be(VolumeUnits.Nm3);
        }
    }

    [Fact]
    public void ZeroVolumeInNm3()
    {
        var newVolume = new Volume(0, VolumeUnits.Nm3);

        using (new AssertionScope())
        {
            newVolume.Value.Should().Be(0);
            newVolume.Unit.Should().Be(VolumeUnits.Nm3);
        }
    }

    [Fact]
    public void NegativeVolumeMustThrowException()
    {
        Action act = () => _ = new Volume(-10, VolumeUnits.Nm3);

        act.Should().
            Throw<ArgumentOutOfRangeException>().
            WithMessage("Value can't be negative! (Parameter 'value')");

    }

    [Fact]
    public void CheckStringRepresentation()
    {
        using (new AssertionScope())
        {
            var newVolume = new Volume(1000, VolumeUnits.Nm3);
            newVolume.ToString().Should().Be("{\r\n  \"Value\": 1000.0,\r\n  \"Unit\": \"Nm3\"\r\n}");

            var newVolume2 = new Volume(575, VolumeUnits.Nm3);
            newVolume2.ToString().Should().Be("{\r\n  \"Value\": 575.0,\r\n  \"Unit\": \"Nm3\"\r\n}");
        }
    }

    [Fact]
    public void CheckDoubleConversion()
    {
        var newVolume = new Volume(575, VolumeUnits.Nm3);

        using (new AssertionScope())
        {
            var originalUnit = newVolume.GetVolumeValue(VolumeUnits.Nm3);
            originalUnit.Should().Be(575);

            var valueInKilogram = newVolume.GetVolumeValue(VolumeUnits.NL);
            valueInKilogram.Should().Be(575000);

            var act = () => _ = newVolume.GetVolumeValue((VolumeUnits)int.MaxValue);
            act.Should().Throw<NotImplementedException>().WithMessage("Conversion between Nm3 and 2147483647 is not implemented yet!");
        }
    }
}
