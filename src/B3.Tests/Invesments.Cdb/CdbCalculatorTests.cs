using B3.Investments.Cdb.Models;
using B3.Investments.Cdb.Services;
using B3.Shared.Results;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace B3.Tests.Invesments.Cdb;
public class CdbCalculatorTests
{
    [Fact]
    public void CalculateCdb_InvalidInvestment_ReturnsValidationResult()
    {

        var investment = new CdbInvestment();

        var validator = Substitute.For<ICdbInvestmentValidator>();
        var validationErrors = new ValidationResult();
        validationErrors.AddError("InitialValue", "Investir em CDB requer um investimento inicial.");
        validator.Validate(investment).Returns(validationErrors);

        var taxRatesOptions = Options.Create(new CdbTaxRatesPerMonths());
        var taxOptionsOptions = Options.Create(new CdbTaxOptions());

        var calculator = new CdbCalculator(validator, taxRatesOptions, taxOptionsOptions);

        var calculationResult = calculator.CalculateCdb(investment);


        Assert.False(calculationResult.IsSuccess);
        ValidationResult validationResult = default!;
        calculationResult.Match(null!, t => validationResult = t);

        Assert.Single(validationResult!.Errors);
        Assert.Equal(validationErrors.Errors[0].PropertyName, validationResult.Errors[0].PropertyName);
        Assert.Equal(validationErrors.Errors[0].Message, validationResult.Errors[0].Message);
    }

    [Theory]
    [InlineData(4, 1039.45, 1030.57)]
    [InlineData(10, 1101.56, 1081.25)]
    [InlineData(18, 1190.19, 1156.91)]
    [InlineData(36, 1416.56, 1354.07)]
    public void CalculateCdb_ValidInvestment_ReturnsCdbIncome(int redeemTermMonths, decimal grossValue, decimal netValue)
    {
        var investment = new CdbInvestment
        {
            InitialValue = 1000,
            RedeemTermMonths = redeemTermMonths
        };

        var validator = Substitute.For<ICdbInvestmentValidator>();
        validator.Validate(investment).Returns(new ValidationResult());

        var taxRates = new CdbTaxRatesPerMonths();
        var taxOptions = new CdbTaxOptions();
        var taxRatesOptions = Options.Create(taxRates);
        var taxOptionsOptions = Options.Create(taxOptions);

        var calculator = new CdbCalculator(validator, taxRatesOptions, taxOptionsOptions);

        var result = calculator.CalculateCdb(investment);

        Assert.True(result.IsSuccess);

        CdbIncome cdbIncome = default!;
        result.Match(t => cdbIncome = t, null!);

        Assert.Equal(grossValue, Math.Round(cdbIncome.Gross, 2));
        Assert.Equal(netValue, Math.Round(cdbIncome.Net, 2));
    }
}
