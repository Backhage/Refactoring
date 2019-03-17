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
            decimal amount;
            switch (Play.Type)
            {
                case "tragedy":
                    amount = 40_000m;
                    if (_performance.Audience > 30)
                    {
                        amount += 1_000 * (_performance.Audience - 30);
                    }
                    break;
                case "comedy":
                    amount = 30_000m;
                    if (_performance.Audience > 20)
                    {
                        amount += 10_000 + 500 * (_performance.Audience - 20);
                    }
                    amount += 300 * _performance.Audience;
                    break;
                default:
                    throw new ArgumentException($"unknown type: {Play.Type}");
            }

            return amount;
        }
    }
}
