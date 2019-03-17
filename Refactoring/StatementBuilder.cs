using Refactoring.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
            var result = new StringBuilder();
            result.Append($"Statement for {data.Customer}{Environment.NewLine}");

            foreach (var perf in data.Performances)
            {
                result.Append($"  {perf.Play.Name}: {Usd(perf.Amount)} ({perf.Audience} seats){Environment.NewLine}");
            }

            result.Append($"Amount owed is {Usd(data.TotalAmount)}{Environment.NewLine}");
            result.Append($"You earned {data.TotalVolumeCredits} credits{Environment.NewLine}");
            return result.ToString();

            string Usd(decimal aNumber)
            {
                return (aNumber / 100).ToString("C", new CultureInfo("en-US"));
            }
        }

        private static string RenderHtml(StatementData data)
        {
            var result = $"<h1>Statement for {data.Customer}</h1>{Environment.NewLine}"
                + $"<table>{Environment.NewLine}"
                + $"<tr><th>play</th><th>seats</th><th>cost</th></tr>";

            return result;
        }
    }
}
