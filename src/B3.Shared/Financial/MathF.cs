namespace B3.Shared.Financial;

public static class MathF
{
    public static decimal WithCompoundInterest(this decimal value, decimal tax, int term)
    {
        if (tax < 0) throw new ArgumentOutOfRangeException(nameof(tax));
        if (term <= 0) throw new ArgumentOutOfRangeException(nameof(term));

        return value * ((1 + tax).Pow(term));
    }

    public static decimal CalculateIncome(this decimal grossIncome, decimal investment)
    {
        var res = grossIncome - investment;
        if (res < 0) res = 0;
        return res;
    }

    public static decimal CalculateIncomeTax(this decimal grossIncome, decimal investment, decimal tax)
    {
        var totalIncome = grossIncome.CalculateIncome(investment);
        return totalIncome * tax;
    }

    public static decimal GetNetIncomeAfterIncomeTax(this decimal grossIncome, decimal investment, decimal tax)
    {
        var incomeTax = grossIncome.CalculateIncomeTax(investment, tax);
        return grossIncome - incomeTax;
    }


    public static decimal Pow(this decimal value, int pow)
    {
        decimal result = 1;

        for (int i = 0; i < pow; i++)
        {
            result *= value;
        }

        return result;
    }
}
