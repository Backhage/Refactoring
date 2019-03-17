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
            var totalAmount = 0m;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}{Environment.NewLine}";
            var format = new CultureInfo("en-US").NumberFormat;

            foreach (var perf in invoice.Performances)
            {
                var play = PlayFor(perf);
                var thisAmount = 0m;

                thisAmount = AmountFor(perf, play);

                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten attendees
                if ("comedy" == play.Type) volumeCredits += perf.Audience / 5;

                // print line for this order
                result += $"  {play.Name}: {(thisAmount / 100).ToString("C", format)} ({perf.Audience} seats){Environment.NewLine}";
                totalAmount += thisAmount;
            }
            result += $"Amount owed is {(totalAmount / 100).ToString("C", format)}{Environment.NewLine}";
            result += $"You earned {volumeCredits} credits{Environment.NewLine}";
            return result;

            Play PlayFor(Invoice.Performance aPerformance)
            {
                return plays.Single(p => p.PlayId == aPerformance.PlayId);
            }
        }

        private static decimal AmountFor(Invoice.Performance aPerformance, Play play)
        {
            decimal result;
            switch (play.Type)
            {
                case "tragedy":
                    result = 40_000m;
                    if (aPerformance.Audience > 30)
                    {
                        result += 1_000 * (aPerformance.Audience - 30);
                    }
                    break;
                case "comedy":
                    result = 30_000m;
                    if (aPerformance.Audience > 20)
                    {
                        result += 10_000 + 500 * (aPerformance.Audience - 20);
                    }
                    result += 300 * aPerformance.Audience;
                    break;
                default:
                    throw new ArgumentException($"unknown type: {play.Type}");
            }

            return result;
        }
    }
}
