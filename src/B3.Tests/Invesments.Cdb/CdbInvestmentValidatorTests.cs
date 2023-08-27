using B3.Investments.Cdb.Models;
using B3.Investments.Cdb.Services;
using B3.Shared.Results;

namespace B3.Tests.Invesments.Cdb;

public class CdbInvestmentValidatorTests
{
    [Fact]
    public void Validate_NullInvestment_ReturnsError()
    {
        var validator = new CdbInvestmentValidator();

        var result = validator.Validate(null);

        Assert.Single(result.Errors);
        Assert.Equal("investment", result.Errors[0].PropertyName);
        Assert.Equal("Nenhum propriedade do investimento foi definida.", result.Errors[0].Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]    
    [InlineData(-100)]    
    public void Validate_InvalidInitialValue_ReturnsError(decimal invalidValue)
    {
        var validator = new CdbInvestmentValidator();
        var investment = new CdbInvestment { InitialValue = invalidValue, RedeemTermMonths = 3 };
               
        var result = validator.Validate(investment);
        
        Assert.Single(result.Errors);
        Assert.Equal("InitialValue", result.Errors[0].PropertyName);
        Assert.Equal("Investir em CDB requer um investimento inicial.", result.Errors[0].Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void Validate_ValidInitialValue_ReturnsNoError(decimal validValue)
    {        
        var validator = new CdbInvestmentValidator();
        var investment = new CdbInvestment { InitialValue = validValue, RedeemTermMonths = 3 };
                
        var result = validator.Validate(investment);        
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData(0, "O prazo mínimo para resgate de um CDB é de 2 meses.")]
    [InlineData(-1, "O prazo mínimo para resgate de um CDB é de 2 meses.")]
    [InlineData(1, "O prazo mínimo para resgate de um CDB é de 2 meses.")]
    [InlineData(61, "O prazo máximo para resgate de um CDB é de 60 meses.")]
    public void Validate_ValidRedeemTermMonths_ReturnsError(int redeemTermMonths, string msg)
    {
        var validator = new CdbInvestmentValidator();
        var investment = new CdbInvestment { InitialValue = 100m, RedeemTermMonths = redeemTermMonths };

        var result = validator.Validate(investment);

        Assert.Single(result.Errors);
        Assert.Equal("RedeemTermMonths", result.Errors[0].PropertyName);
        Assert.Equal(msg, result.Errors[0].Message);
    }    


    [Theory]
    [InlineData(2)]
    [InlineData(60)]
    public void Validate_ValidRedeemTermMonths_ReturnsNoError(int redeemTermMonths)
    {
        var validator = new CdbInvestmentValidator();
        var investment = new CdbInvestment { InitialValue = 100m, RedeemTermMonths = redeemTermMonths };

        var result = validator.Validate(investment);
        Assert.Empty(result.Errors);
    }
}
