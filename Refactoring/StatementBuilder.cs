using Refactoring.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using static Refactoring.StatementDataCreation;

namespace Refactoring
{
    public static class StatementBuilder
    {
        public static string Statement(Invoice invoice, IEnumerable<Play> plays)
        {
            return RenderPlainText(CreateStatementData(invoice, plays));
        }

        public static string HtmlStatement(Invoice invoice, IEnumerable<Play> plays)
        {
            return RenderHtml(CreateStatementData(invoice, plays));
        }

        private static string RenderPlainText(StatementData data)
        {
            var result = $"Statement for {data.Customer}{Environment.NewLine}";

            foreach (var perf in data.Performances)
            {
                result += $"  {perf.Play.Name}: {Usd(perf.Amount)} ({perf.Audience} seats){Environment.NewLine}";
            }

            result += $"Amount owed is {Usd(data.TotalAmount)}{Environment.NewLine}";
            result += $"You earned {data.TotalVolumeCredits} credits{Environment.NewLine}";
            return result;

            string Usd(decimal aNumber)
            {
                return (aNumber / 100).ToString("C", new CultureInfo("en-US"));
            }
        }

        private static string RenderHtml(StatementData data)
        {
            return string.Empty;
        }
    }
}
