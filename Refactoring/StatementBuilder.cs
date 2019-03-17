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
            var result = $"Statement for {invoice.Customer}{Environment.NewLine}";

            foreach (var perf in invoice.Performances)
            {
                result += $"  {PlayFor(perf).Name}: {Usd(AmountFor(perf))} ({perf.Audience} seats){Environment.NewLine}";
            }

            result += $"Amount owed is {Usd(TotalAmount())}{Environment.NewLine}";
            result += $"You earned {TotalVolumeCredits()} credits{Environment.NewLine}";
            return result;

            decimal TotalAmount()
            {
                return invoice.Performances.Sum(p => AmountFor(p));
            }
            int TotalVolumeCredits()
            {
                return invoice.Performances.Sum(p => VolumeCreditsFor(p));
            }
            string Usd(decimal aNumber)
            {
                return (aNumber / 100).ToString("C", new CultureInfo("en-US"));
            }
            int VolumeCreditsFor(Invoice.Performance aPerformance)
            {
                var credits = 0;
                credits += Math.Max(aPerformance.Audience - 30, 0);
                if ("comedy" == PlayFor(aPerformance).Type) credits += aPerformance.Audience / 5;
                return credits;
            }
            Play PlayFor(Invoice.Performance aPerformance)
            {
                return plays.Single(p => p.PlayId == aPerformance.PlayId);
            }
            decimal AmountFor(Invoice.Performance aPerformance)
            {
                decimal amount;
                switch (PlayFor(aPerformance).Type)
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
                        throw new ArgumentException($"unknown type: {PlayFor(aPerformance).Type}");
                }

                return amount;
            }
        }
    }
}
