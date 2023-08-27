using B3.Investments.Cdb.Models;
using B3.Shared.Results;
using MediatR;

namespace webapi.Handlers;

public sealed class CdbCalculationRequest : IRequest<Result<CdbIncome, ValidationResult>>
{
    public CdbInvestment? Investment { get; set; }
}
