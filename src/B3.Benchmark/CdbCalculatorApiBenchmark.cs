using B3.Investments.Cdb.Models;
using B3.Shared.Results;
using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using System.Net.Http.Json;

namespace B3.Benchmark;

[MemoryDiagnoser(false)]
public class CdbCalculatorApiBenchmark
{
    private readonly HttpClient http;

    public CdbCalculatorApiBenchmark()
    {
        http = new HttpClient();        
    }

    public sealed class CdbCalculationRequest 
    {
        public CdbInvestment? Investment { get; set; }
    }

    [Benchmark]
    public async Task<HttpResponseMessage> CalculateCdb()
    {
        var req = new CdbCalculationRequest()
        {
            Investment = new CdbInvestment()
            {
                InitialValue = 1000,
                RedeemTermMonths = 0,
            }
        };       

        HttpResponseMessage res =  default!;

        for (int i = 2; i < 61; i++)
        {
            req.Investment.RedeemTermMonths = i;
            res = await http.PostAsJsonAsync("http://localhost:5138/cdbCalculation", req);
        }
                
        return res;
    }

    [Benchmark]
    public async Task<HttpResponseMessage> CalculateCdbV2()
    {
        var req = new CdbCalculationRequest()
        {
            Investment = new CdbInvestment()
            {
                InitialValue = 1000,
                RedeemTermMonths = 0,
            }
        };

        HttpResponseMessage res = default!;

        for (int i = 2; i < 61; i++)
        {
            req.Investment.RedeemTermMonths = i;
            res = await http.PostAsJsonAsync("http://localhost:5138/cdbv2", req);
        }

        return res;
    }

    [Benchmark]
    public async Task<HttpResponseMessage> CalculateCdbFunctional()
    {
        var req = new CdbCalculationRequest()
        {
            Investment = new CdbInvestment()
            {
                InitialValue = 1000,
                RedeemTermMonths = 0,
            }
        };

        HttpResponseMessage res = default!;

        for (int i = 2; i < 61; i++)
        {
            req.Investment.RedeemTermMonths = i;
            res = await http.PostAsJsonAsync("http://localhost:5138/cdbfunctional", req);
        }

        return res;
    }

    
}
