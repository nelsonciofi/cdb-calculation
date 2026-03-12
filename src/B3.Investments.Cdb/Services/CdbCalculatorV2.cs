using B3.Investments.Cdb.Models;
using B3.Shared.Financial;
using B3.Shared.Results;
using Microsoft.Extensions.Options;

namespace B3.Investments.Cdb.Services;

public class CdbCalculatorV2 : ICdbCalculatorV2
{
    private readonly ICdbInvestmentValidator cdbInvestmentValidator;   

    private readonly decimal[] interestRates;
    private readonly decimal[] taxRates;

    public CdbCalculatorV2(ICdbInvestmentValidator cdbInvestmentValidator,
                           IOptions<CdbTaxRatesPerMonths> taxRatesPerMonths,
                           IOptions<CdbTaxOptions> cdbTaxOptions)
    {
        this.cdbInvestmentValidator = cdbInvestmentValidator;        

        var baseInterestRate = cdbTaxOptions.Value.Cdi * cdbTaxOptions.Value.Tb;
        interestRates = ConfigureInterestRates(baseInterestRate);
        taxRates = ConfigureTaxRates(taxRatesPerMonths.Value);
    }

    private static decimal[] ConfigureInterestRates(decimal interestRate)
    {
        var ir = new decimal[61];

        ir[0] = 0m;

        for (int i = 1; i < 60; i++)
        {

            ir[i] = 1m.WithCompoundInterest(interestRate, i);
        }

        return ir;
    }

    private static decimal[] ConfigureTaxRates(CdbTaxRatesPerMonths taxRatesPerMonths)
    {
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
    

    public Result<CdbIncome, ValidationResult> CalculateCdb(CdbInvestment? investment)
    {
        var validation = cdbInvestmentValidator.Validate(investment);
        if (!validation.IsValid)
        {
            return validation;
        }

        var initialValue = investment!.InitialValue;
        var redeemTermMonth = investment!.RedeemTermMonths;

        var compoundInterest = interestRates[redeemTermMonth];
        var taxRate = taxRates[redeemTermMonth];

        var grossValue = initialValue * compoundInterest;        
        var netValue = grossValue - ((grossValue - initialValue) * taxRate);

        return new CdbIncome
        {
            Gross = Math.Round(grossValue, 2),
            Net = Math.Round(netValue, 2),
        };
    }
}