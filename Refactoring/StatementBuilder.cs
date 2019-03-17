using Refactoring.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Refactoring
{
    public class StatementBuilder
    {
        public static string Statement(Invoice invoice, IEnumerable<Play> plays)
        {
            var statementData = new StatementData
            {
                Customer = invoice.Customer,
                Performances = invoice.Performances.Select(EnrichPerformance)
            };

            return RenderPlainText(statementData, plays);

            EnrichedPerformance EnrichPerformance(Invoice.Performance performance)
            {
                var result = new EnrichedPerformance(performance);
                result.Play = PlayFor(result);
                result.Amount = AmountFor(result);
                result.VolumeCredits = VolumeCreditsFor(result);
                return result;
            }

            Play PlayFor(Invoice.Performance aPerformance)
            {
                return plays.Single(p => p.PlayId == aPerformance.PlayId);
            }
            decimal AmountFor(EnrichedPerformance aPerformance)
            {
                decimal amount;
                switch (aPerformance.Play.Type)
                {
                    case "tragedy":
                        amount = 40_000m;
                        if (aPerformance.Audience > 30)
                        {
                            amount += 1_000 * (aPerformance.Audience - 30);
                        }
                        break;
                    case "comedy":
                        amount = 30_000m;
                        if (aPerformance.Audience > 20)
                        {
                            amount += 10_000 + 500 * (aPerformance.Audience - 20);
                        }
                        amount += 300 * aPerformance.Audience;
                        break;
                    default:
                        throw new ArgumentException($"unknown type: {aPerformance.Play.Type}");
                }

                return amount;
            }
            int VolumeCreditsFor(EnrichedPerformance aPerformance)
            {
                var credits = 0;
                credits += Math.Max(aPerformance.Audience - 30, 0);
                if ("comedy" == aPerformance.Play.Type) credits += aPerformance.Audience / 5;
                return credits;
            }
        }

        private class StatementData
        {
            public string Customer { get; set; }
            public IEnumerable<EnrichedPerformance> Performances { get; set; }
        }

        private class EnrichedPerformance : Invoice.Performance
        {
            public Play Play { get; set; }
            public decimal Amount { get; set; }
            public int VolumeCredits { get; set; }

            public EnrichedPerformance(Invoice.Performance performance)
                : base(performance.PlayId, performance.Audience)
            {
            }
        }

        private static string RenderPlainText(StatementData data, IEnumerable<Play> plays)
        {
            var result = $"Statement for {data.Customer}{Environment.NewLine}";

            foreach (var perf in data.Performances)
            {
                result += $"  {perf.Play.Name}: {Usd(perf.Amount)} ({perf.Audience} seats){Environment.NewLine}";
            }

            result += $"Amount owed is {Usd(TotalAmount())}{Environment.NewLine}";
            result += $"You earned {TotalVolumeCredits()} credits{Environment.NewLine}";
            return result;

            decimal TotalAmount()
            {
                return data.Performances.Sum(p => p.Amount);
            }
            int TotalVolumeCredits()
            {
                return data.Performances.Sum(p => p.VolumeCredits);
            }
            string Usd(decimal aNumber)
            {
                return (aNumber / 100).ToString("C", new CultureInfo("en-US"));
            }
        }
    }
}
