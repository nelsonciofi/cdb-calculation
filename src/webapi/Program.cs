using B3.Investments.Cdb.Services;
using Microsoft.AspNetCore.Mvc;
using webapi.Handlers;
using webapi.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.AddCdb();

builder.Services.AddControllers().CustomizeDefaultModelStateValidationResult();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(cfg => cfg.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();



app.MapPost("cdbv2", ([FromBody] CdbCalculationRequest req, ICdbCalculatorV2 cdbV2) =>
{
    var res = cdbV2.CalculateCdb(req.Investment);

    return res.Match(income => Results.Ok(income),
                     errors => Results.BadRequest(errors));
});


app.MapPost("cdbfunctional", ([FromBody] CdbCalculationRequest req) =>
{
    return
    FunctionalCdbInvestmentValidator.Validate(req.Investment)
                                    .Match(investment => FunctionalCdbCalculator.CalculateCdb(investment)
                                                                                .Match(income => Results.Ok(income),     
                                                                                       errors => Results.BadRequest(errors)),
                                           errors => Results.BadRequest(errors));
    
});

app.Run();
