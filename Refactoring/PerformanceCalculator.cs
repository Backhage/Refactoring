using Refactoring.Models;
using System;
using static Refactoring.Models.Invoice;

namespace Refactoring
{
    internal class PerformanceCalculator
    {
        private readonly Performance _performance;

        public PerformanceCalculator(Performance aPerformance, Play aPlay)
        {
            _performance = aPerformance;
            Play = aPlay;
        }

        public Play Play { get; }
        public decimal Amount
        {
            get
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

        public int VolumeCredits
        {
            get
            {
                var credits = 0;
                credits += Math.Max(_performance.Audience - 30, 0);
                if ("comedy" == Play.Type) credits += _performance.Audience / 5;
                return credits;
            }
        }
    }

    internal class TragedyCalculator : PerformanceCalculator
    {
        public TragedyCalculator(Performance aPerformance, Play aPlay)
            : base(aPerformance, aPlay)
        {
        }
    }

    internal class ComedyCalculator : PerformanceCalculator
    {
        public ComedyCalculator(Performance aPerformance, Play aPlay)
            : base(aPerformance, aPlay)
        {
        }
    }
}
