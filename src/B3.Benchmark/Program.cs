
using B3.Benchmark;
using BenchmarkDotNet.Running;



BenchmarkRunner.Run<CdbCalculatorApiBenchmark>();
Console.ReadLine();
BenchmarkRunner.Run<CdbCalculatorBenchmark>();