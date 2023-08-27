using B3.Investments.Cdb.Models;
using B3.Shared.Financial;
using B3.Shared.Results;
using Microsoft.Extensions.Options;

namespace B3.Investments.Cdb.Services;

public class CdbCalculator : ICdbCalculator
{
    private readonly ICdbInvestmentValidator cdbInvestmentValidator;
    private readonly CdbTaxRatesPerMonths taxRatesPerMonths;

    private readonly decimal interestRate;

    public CdbCalculator(ICdbInvestmentValidator cdbInvestmentValidator,
                         IOptions<CdbTaxRatesPerMonths> taxRatesPerMonths,
                         IOptions<CdbTaxOptions> cdbTaxOptions)
    {
        this.cdbInvestmentValidator = cdbInvestmentValidator;
        this.taxRatesPerMonths = taxRatesPerMonths.Value;
        interestRate = cdbTaxOptions.Value.Cdi * cdbTaxOptions.Value.Tb;
    }

    public Result<CdbIncome, ValidationResult> CalculateCdb(CdbInvestment? investment)
    {
        var validation = cdbInvestmentValidator.Validate(investment);
        if (!validation.IsValid)
        {
            return validation;
        }
        var grossValue = investment!.InitialValue.WithCompoundInterest(interestRate, investment.RedeemTermMonths);
        var taxRate = GetTaxRatePerTerm(investment.RedeemTermMonths);
        var netValue = grossValue.GetNetIncomeAfterIncomeTax(investment.InitialValue, taxRate);
        return new CdbIncome
        {
            Gross = Math.Round(grossValue, 2),
            Net = Math.Round(netValue,2),
        };
    }

    private decimal GetTaxRatePerTerm(int term)
        => term switch
        {
            > 24 => taxRatesPerMonths.Over24Months,
            > 12 => taxRatesPerMonths.UpTo24Months,
            > 6 => taxRatesPerMonths.UpTo12Months,
            _ => taxRatesPerMonths.UpTo06Months
        };
}
