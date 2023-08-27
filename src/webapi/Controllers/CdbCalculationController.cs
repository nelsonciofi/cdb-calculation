using MediatR;
using Microsoft.AspNetCore.Mvc;
using webapi.Handlers;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CdbCalculationController : ControllerBase
{
    private readonly IMediator mediator;

    public CdbCalculationController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> CalculateCdb(CdbCalculationRequest request)
    {
        var res = await mediator.Send(request);
        
        return res.Match<IActionResult>(income => Ok(income),
                                        errors => BadRequest(errors));
    }
}
