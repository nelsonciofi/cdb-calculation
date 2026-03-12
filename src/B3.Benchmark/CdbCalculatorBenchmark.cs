using B3.Investments.Cdb.Models;
using B3.Investments.Cdb.Services;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Options;

namespace B3.Benchmark;

[MemoryDiagnoser(true)]
public class CdbCalculatorBenchmark
{
    private readonly CdbCalculator cdbCalculator;
    private readonly CdbCalculatorV2 cdbCalculatorV2;

    public CdbCalculatorBenchmark()
    {
        var validator = new CdbInvestmentValidator();
        var taxRatesPerMonth = Options.Create(new CdbTaxRatesPerMonths());
        var taxOptions = Options.Create(new CdbTaxOptions());

        cdbCalculator = new CdbCalculator(validator, taxRatesPerMonth, taxOptions);
        cdbCalculatorV2 = new CdbCalculatorV2(validator, taxRatesPerMonth, taxOptions);
    }

    [Benchmark]
    public bool CalculateCdb()
    {
        var investment = new CdbInvestment()
        {
            InitialValue = 1000,
            RedeemTermMonths = 0,
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 2; j < 60; j++)
            {
                investment.RedeemTermMonths = i;
                _ = cdbCalculator.CalculateCdb(investment);
            }
        }

        return investment.RedeemTermMonths == 60;
    }

    [Benchmark]
    public bool CalculateCdbV2()
    {
        var investment = new CdbInvestment()
        {
            InitialValue = 1000,
            RedeemTermMonths = 0,
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 2; j < 60; j++)
            {
                investment.RedeemTermMonths = i;
                _ = cdbCalculatorV2.CalculateCdb(investment);
            }
        }

        return investment.RedeemTermMonths == 60;
    }


    [Benchmark]
    public bool CalculateCdbFunctional()
    {
        var investment = new CdbInvestment()
        {
            InitialValue = 1000,
            RedeemTermMonths = 0,
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 2; j < 60; j++)
            {
                investment.RedeemTermMonths = i;
                _ = FunctionalCdbCalculator.CalculateCdb(investment);
            }
        }

        return investment.RedeemTermMonths == 60;
    }
}
