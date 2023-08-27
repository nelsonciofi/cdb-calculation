using B3.Investments.Cdb.Models;
using B3.Shared.Results;

namespace B3.Investments.Cdb.Services;

public interface ICdbInvestmentValidator
{
    ValidationResult Validate(CdbInvestment? investment);
}
