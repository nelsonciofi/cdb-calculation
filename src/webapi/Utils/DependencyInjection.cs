using B3.Investments.Cdb.Models;
using B3.Investments.Cdb.Services;
using B3.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace webapi.Utils;

public static class DependencyInjection
{
    public static void AddCdb(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;
        
        services.Configure<CdbTaxOptions>(configuration.GetSection("CdbTaxOptions"))
                .Configure<CdbTaxRatesPerMonths>(configuration.GetSection("CdbTaxRatesPerMonths"))
                .AddSingleton<ICdbCalculator, CdbCalculator>()
                .AddSingleton<ICdbInvestmentValidator, CdbInvestmentValidator>();
    }

    public static void CustomizeDefaultModelStateValidationResult(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var res = actionContext.ModelState.Values.SelectMany(v => v.Errors).ToValidationResult();              


                return new UnprocessableEntityObjectResult(res);
            };
        });
    }

    private static ValidationResult ToValidationResult(this IEnumerable<ModelError> modelErros)
    {
        var res = new ValidationResult();

        foreach (var me in modelErros)
        {
            res.AddError("ModelState", me.ErrorMessage);
        }

        return res;
    }
}
