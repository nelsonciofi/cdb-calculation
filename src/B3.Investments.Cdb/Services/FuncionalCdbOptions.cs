using B3.Investments.Cdb.Models;
using B3.Shared.Financial;

namespace B3.Investments.Cdb.Services;

public static class FuncionalCdbOptions
{
    public static decimal[] InterestRates => interestRates;
    public static decimal[] TaxRates => taxRates;



    private static readonly decimal[] interestRates = ConfigureInterestRates();
    private static readonly decimal[] taxRates = ConfigureTaxRates();

    private static decimal[] ConfigureInterestRates()
    {
        var cdbTaxOptions = new CdbTaxOptions();
        var interestRate = cdbTaxOptions.Cdi * cdbTaxOptions.Tb;       

        var ir = new decimal[61];

        ir[0] = 0m;

        for (int i = 1; i < 60; i++)
        {

            ir[i] = 1m.WithCompoundInterest(interestRate, i);
        }

        return ir;
    }

    private static decimal[] ConfigureTaxRates()
    {
        var taxRatesPerMonths = new CdbTaxRatesPerMonths();

        var tr = new decimal[61];

        for (int i = 0; i < 60; i++)
        {
            tr[i] = i switch
            {
                > 24 => taxRatesPerMonths.Over24Months,
                > 12 => taxRatesPerMonths.UpTo24Months,
                > 6 => taxRatesPerMonths.UpTo12Months,
                _ => taxRatesPerMonths.UpTo06Months
            };
        }

        return tr;
    }
}