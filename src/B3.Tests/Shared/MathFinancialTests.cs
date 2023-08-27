using B3.Shared.Financial;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace B3.Tests.Shared;
public class MathFinancialTests
{
    [Theory]
    [InlineData(1000, 0.05, 12, 1795.85632602)]
    [InlineData(5000, 0.08, 24, 31705.90368620)]
    [InlineData(1500, 0.12, 6, 2960.73402778)]
    public void WithCompoundInterest_ValidInput_ReturnsExpectedResult(decimal initialValue,
                                                                      decimal tax,
                                                                      int term,
                                                                      decimal expected)
    {        
        var result = initialValue.WithCompoundInterest(tax, term);
                
        Assert.Equal(expected, Math.Round(result, 8));
    }

    [Fact]
    public void WithCompoundInterest_ZeroInitialValue_ReturnsZero()
    {
        var initialValue = 0m;
        var tax = 0.05m;
        var term = 12;
                
        var result = initialValue.WithCompoundInterest(tax, term);
                
        Assert.Equal(0, result);
    }

    [Fact]
    public void WithCompoundInterest_ZeroTax_ReturnsInitialValue()
    {        
        var initialValue = 2000m;
        var tax = 0m;
        var term = 6;
                
        var result = initialValue.WithCompoundInterest(tax, term);
               
        Assert.Equal(initialValue, result);
    }

    [Fact]
    public void WithCompoundInterest_NegativeTax_ThrowsException()
    {        
        var initialValue = 1500m;
        var negativeTax = -0.1m;
        var term = 24;
        
        Assert.Throws<ArgumentOutOfRangeException>(() => initialValue.WithCompoundInterest(negativeTax, term));
    }

    [Fact]
    public void WithCompoundInterest_NegativeTerm_ThrowsException()
    {        
        var initialValue = 1000m;
        var tax = 0.06m;
        var negativeTerm = -3;
                
        Assert.Throws<ArgumentOutOfRangeException>(() => initialValue.WithCompoundInterest(tax, negativeTerm));
    }

    [Theory]
    [InlineData(2000, 1500, 500)]
    [InlineData(1000, 800, 200)]
    [InlineData(500, 600, 0)]
    [InlineData(0, 0, 0)]
    public void CalculateIncome_ValidInput_ReturnsExpectedResult(decimal grossIncome,
                                                                 decimal investment,
                                                                 decimal expected)
    {        
        var result = grossIncome.CalculateIncome(investment);
        
        Assert.Equal(expected, result);
    }

    [Fact]    
    public void CalculateIncome_WithLossInput_ReturnsZero()
    {
        var grossIncome = 500m;

        var result = grossIncome.CalculateIncome(600);

        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData(2, 3, 8)]
    [InlineData(3, 4, 81)]
    [InlineData(2, 0, 1)]
    [InlineData(4, 1, 4)]
    [InlineData(1, 10, 1)]
    public void Pow_ValidInput_ReturnsExpectedResult(decimal value,
                                                     int pow,
                                                     decimal expected)
    {        
        var result = value.Pow(pow);
                
        Assert.Equal(expected, result);
    }
    

    [Fact]
    public void Pow_ZeroPower_ReturnsOne()
    {
        decimal value = 5;
        int pow = 0;

        var result = value.Pow(pow);

        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData(1000, 150, 0.15, 872.5)]
    [InlineData(2000, 300, 0.2, 1660)]
    [InlineData(500, 100, 0.1, 460)]
    public void GetNetIncomeAfterIncomeTax_ValidInput_ReturnsExpectedResult(decimal grossIncome, decimal investment, decimal tax, decimal expectedNetIncome)
    {        
        var result = grossIncome.GetNetIncomeAfterIncomeTax(investment, tax);

        Assert.Equal(expectedNetIncome, result);
    }

    [Fact]
    public void CalculateIncomeTax_ZeroGrossIncome_ReturnsZero()
    {
        decimal grossIncome = 0;
        decimal investment = 50;
        decimal tax = 0.1m;

        var result = grossIncome.CalculateIncomeTax(investment, tax);

        Assert.Equal(0, result);
    }
    

    [Fact]
    public void GetNetIncomeAfterIncomeTax_ZeroTax_ReturnsGrossIncome()
    {
        decimal grossIncome = 1200;
        decimal investment = 150;
        decimal zeroTax = 0;

        var result = grossIncome.GetNetIncomeAfterIncomeTax(investment, zeroTax);

        Assert.Equal(grossIncome, result);
    }

    [Fact]
    public void GetNetIncomeAfterIncomeTax_ZeroGrossIncome_ReturnsZero()
    {
        decimal grossIncome = 0;
        decimal investment = 50;
        decimal tax = 0.1m;

        var result = grossIncome.GetNetIncomeAfterIncomeTax(investment, tax);

        Assert.Equal(0, result);
    }
}




