namespace blast_furnace_mass_balance_lib;

public class Weight
{
    public double Value { get; private set; }

    public WeightUnits Unit { get; private set; }

    public Weight(double value, WeightUnits units)
    {
        Value = value;
        Unit = units;
    }
}