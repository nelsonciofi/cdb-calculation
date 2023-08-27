namespace B3.Investments.Cdb.Models;

public sealed class CdbTaxRatesPerMonths
{
    public decimal UpTo06Months { get; set; } = 0.225m;
    public decimal UpTo12Months { get; set; } = 0.200m;
    public decimal UpTo24Months { get; set; } = 0.175m;
    public decimal Over24Months { get; set; } = 0.15m;
}
