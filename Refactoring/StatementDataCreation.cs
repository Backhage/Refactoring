using Refactoring.Models;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    internal static class StatementDataCreation
    {
        public static StatementData CreateStatementData(Invoice invoice, IEnumerable<Play> plays)
        {
            var result = new StatementData();
            result.Customer = invoice.Customer;
            result.Performances = invoice.Performances.Select(EnrichPerformance);
            result.TotalAmount = TotalAmount(result);
            result.TotalVolumeCredits = TotalVolumeCredits(result);
            return result;

            EnrichedPerformance EnrichPerformance(Performance aPerformance)
            {
                var calculator = PerformanceCalculatorFactory.Create(aPerformance, PlayFor(aPerformance));
                return new EnrichedPerformance(aPerformance)
                {
                    Play = calculator.Play,
                    Amount = calculator.Amount,
                    VolumeCredits = calculator.VolumeCredits
                };
            }

            Play PlayFor(Performance aPerformance)
            {
                return plays.Single(p => p.PlayId == aPerformance.PlayId);
            }

            decimal TotalAmount(StatementData data)
            {
                return data.Performances.Sum(p => p.Amount);
            }

            int TotalVolumeCredits(StatementData data)
            {
                return data.Performances.Sum(p => p.VolumeCredits);
            }
        }

        internal class StatementData
        {
            public string Customer { get; set; }
            public IEnumerable<EnrichedPerformance> Performances { get; set; }
            public decimal TotalAmount { get; set; }
            public int TotalVolumeCredits { get; set; }
        }

        internal class EnrichedPerformance : Performance
        {
            public Play Play { get; set; }
            public decimal Amount { get; set; }
            public int VolumeCredits { get; set; }

            public EnrichedPerformance(Performance performance)
                : base(performance.PlayId, performance.Audience)
            {
            }
        }
    }
}
