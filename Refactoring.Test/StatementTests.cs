using NUnit.Framework;
using Refactoring.Models;
using System;
using System.Collections.Generic;

namespace Refactoring.Test
{
    public class StatementTests
    {
        private List<Play> _plays;
        private Invoice _invoice;

        [SetUp]
        public void SetUp()
        {
            _plays = new List<Play>
            {
                new Play("hamlet", "Hamlet", "tragedy"),
                new Play("as-like", "As You Like It", "comedy"),
                new Play("othello", "Othello", "tragedy")
            };

            _invoice = new Invoice("BigCo");
            _invoice.Add("hamlet", 55);
            _invoice.Add("as-like", 35);
            _invoice.Add("othello", 40);
        }

        [Test]
        public void CreateStatement()
        {
            Assert.AreEqual(
                $"Statement for BigCo{Environment.NewLine}"
                + $"  Hamlet: $650.00 (55 seats){Environment.NewLine}"
                + $"  As You Like It: $580.00 (35 seats){Environment.NewLine}"
                + $"  Othello: $500.00 (40 seats){Environment.NewLine}"
                + $"Amount owed is $1,730.00{Environment.NewLine}"
                + $"You earned 47 credits{Environment.NewLine}",
                StatementBuilder.Statement(_invoice, _plays));
        }

        [Test]
        public void CreateHtmlStatement()
        {
            Assert.AreEqual(
                $"<h1>Statement for BigCo</h1>{Environment.NewLine}"
                + $"<table>{Environment.NewLine}"
                + $"<tr><th>play</th><th>seats</th><th>cost</th></tr>{Environment.NewLine}"
                + $"<tr><td>Hamlet</td><td>55</td><td>$650.00</td><tr>{Environment.NewLine}"
                + $"<tr><td>As You Like It</td><td>35</td><td>$580.00</td><tr>{Environment.NewLine}"
                + $"<tr><td>Othello</td><td>40</td><td>$500.00</td><tr>{Environment.NewLine}"
                + $"</table>{Environment.NewLine}"
                + $"<p>Amound owed is <em>$1,730.00</em></p>{Environment.NewLine}"
                + $"<p>You earned <em>47</em> credits</p>{Environment.NewLine}",
                StatementBuilder.HtmlStatement(_invoice, _plays));
        }
    }
}