using B3.Investments.Cdb.Models;
using B3.Shared.Results;

namespace B3.Investments.Cdb.Services;

public interface ICdbCalculator
{
    Result<CdbIncome, ValidationResult> CalculateCdb(CdbInvestment? investment);
}
