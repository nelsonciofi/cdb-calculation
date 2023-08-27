using B3.Investments.Cdb.Models;
using B3.Investments.Cdb.Services;
using B3.Shared.Results;
using MediatR;

namespace webapi.Handlers;

public sealed class CdbCalculationHandler : IRequestHandler<CdbCalculationRequest, 
                                                            Result<CdbIncome, ValidationResult>>
{
    private readonly ICdbCalculator cdbCalculator;

    public CdbCalculationHandler(ICdbCalculator cdbCalculator)
    {
        this.cdbCalculator = cdbCalculator;
    }

    public Task<Result<CdbIncome, ValidationResult>> Handle(CdbCalculationRequest request,
                                                            CancellationToken cancellationToken)
    {

        var res = cdbCalculator.CalculateCdb(request.Investment);
        return Task.FromResult(res);
    }
}
