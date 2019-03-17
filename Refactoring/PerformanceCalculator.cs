using Refactoring.Models;
using System;
using static Refactoring.Models.Invoice;

namespace Refactoring
{
    internal class PerformanceCalculator
    {
        private readonly Performance _performance;
        public Play Play { get; }

        public PerformanceCalculator(Performance aPerformance, Play aPlay)
        {
            _performance = aPerformance;
            Play = aPlay;
        }

        public decimal Amount()
        {
            decimal result;
            switch (Play.Type)
            {
                case "tragedy":
                    result = 40_000m;
                    if (_performance.Audience > 30)
                    {
                        result += 1_000 * (_performance.Audience - 30);
                    }
                    break;
                case "comedy":
                    result = 30_000m;
                    if (_performance.Audience > 20)
                    {
                        result += 10_000 + 500 * (_performance.Audience - 20);
                    }
                    result += 300 * _performance.Audience;
                    break;
                default:
                    throw new ArgumentException($"unknown type: {Play.Type}");
            }

            return result;
        }
    }
}
