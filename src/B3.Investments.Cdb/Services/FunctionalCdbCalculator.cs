using B3.Investments.Cdb.Models;
using B3.Shared.Results;

namespace B3.Investments.Cdb.Services;

public static class FunctionalCdbCalculator
{
    public static Result<CdbIncome, ValidationResult> CalculateCdb(CdbInvestment investment)
    {
        var initialValue = investment!.InitialValue;
        var redeemTermMonth = investment!.RedeemTermMonths;

        var compoundInterest = FuncionalCdbOptions.InterestRates[redeemTermMonth];
        var taxRate = FuncionalCdbOptions.TaxRates[redeemTermMonth];

        var grossValue = initialValue * compoundInterest;
        var netValue = grossValue - ((grossValue - initialValue) * taxRate);

        return new CdbIncome
        {
            Gross = Math.Round(grossValue, 2),
            Net = Math.Round(netValue, 2),
        };
    }
}
