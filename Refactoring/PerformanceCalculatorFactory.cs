using Refactoring.Models;
using System;

namespace Refactoring
{
    internal static class PerformanceCalculatorFactory
    {
        public static PerformanceCalculator Create(Performance aPerformance, Play aPlay)
        {
            switch (aPlay.Type)
            {
                case "tragedy":
                    return new TragedyCalculator(aPerformance, aPlay);
                case "comedy":
                    return new ComedyCalculator(aPerformance, aPlay);
                default:
                    throw new ArgumentException($"Unknown type: {aPlay.Type}");
            }
        }
    }
}
