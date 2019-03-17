using NUnit.Framework;
using Refactoring.Models;
using System;
using System.Collections.Generic;

namespace Refactoring.Test
{
    public class StatementTests
    {
        [Test]
        public void CreateStatement()
        {
            var plays = new List<Play>
            {
                new Play("hamlet", "Hamlet", "tragedy"),
                new Play("as-like", "As You Like It", "comedy"),
                new Play("othello", "Othello", "tragedy")
            };

            var invoice = new Invoice("BigCo");
            invoice.Add("hamlet", 55);
            invoice.Add("as-like", 35);
            invoice.Add("othello", 40);

            var statement = StatementBuilder.Statement(invoice, plays);

            Assert.AreEqual(
                $"Statement for BigCo{Environment.NewLine}"
                + $"  Hamlet: $650.00 (55 seats){Environment.NewLine}"
                + $"  As You Like It: $580.00 (35 seats){Environment.NewLine}"
                + $"  Othello: $500.00 (40 seats){Environment.NewLine}"
                + $"Amount owed is $1,730.00{Environment.NewLine}"
                + $"You earned 47 credits{Environment.NewLine}",
                statement);
        }
    }
}