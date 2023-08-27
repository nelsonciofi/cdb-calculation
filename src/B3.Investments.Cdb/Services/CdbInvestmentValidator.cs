using B3.Investments.Cdb.Models;
using B3.Shared.Results;

namespace B3.Investments.Cdb.Services;

public class CdbInvestmentValidator : ICdbInvestmentValidator
{
    public ValidationResult Validate(CdbInvestment? investment)
    {
        var res = new ValidationResult();

        if (investment is null)
        {
            res.AddError("investment", "Nenhum propriedade do investimento foi definida.");
            return res;
        }

        if (investment.InitialValue <= 0)
        {
            res.AddError("InitialValue", "Investir em CDB requer um investimento inicial.");
        }

        if (investment.RedeemTermMonths < 2)
        {
            res.AddError("RedeemTermMonths", "O prazo mínimo para resgate de um CDB é de 2 meses.");
        }

        if (investment.RedeemTermMonths > 60)
        {
            res.AddError("RedeemTermMonths", "O prazo máximo para resgate de um CDB é de 60 meses.");
        }

        return res;
    }
}
