using Refactoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    internal static class StatementDataCreation
    {
        public static StatementData CreateStatementData(Invoice invoice, IEnumerable<Play> plays)
        {
            var statementData = new StatementData();
            statementData.Customer = invoice.Customer;
            statementData.Performances = invoice.Performances.Select(EnrichPerformance);
            statementData.TotalAmount = TotalAmount(statementData);
            statementData.TotalVolumeCredits = TotalVolumeCredits(statementData);
            return statementData;

            EnrichedPerformance EnrichPerformance(Invoice.Performance aPerformance)
            {
                var calculator = new PerformanceCalculator(aPerformance, PlayFor(aPerformance));
                var result = new EnrichedPerformance(aPerformance)
                {
                    Play = calculator.Play,
                    Amount = calculator.Amount,
                    VolumeCredits = calculator.VolumeCredits
                };
                return result;
            }

            Play PlayFor(Invoice.Performance aPerformance)
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

        internal class EnrichedPerformance : Invoice.Performance
        {
            public Play Play { get; set; }
            public decimal Amount { get; set; }
            public int VolumeCredits { get; set; }

            public EnrichedPerformance(Invoice.Performance performance)
                : base(performance.PlayId, performance.Audience)
            {
            }
        }
    }
}
