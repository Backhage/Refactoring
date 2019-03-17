using Refactoring.Models;
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
    }
}
