using Refactoring.Models;
using static Refactoring.Models.Invoice;

namespace Refactoring
{
    internal static class PerformanceCalculatorFactory
    {
        public static PerformanceCalculator Create(Performance aPerformance, Play aPlay)
        {
            return new PerformanceCalculator(aPerformance, aPlay);
        }
    }
}
