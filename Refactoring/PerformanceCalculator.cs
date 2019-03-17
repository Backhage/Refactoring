using Refactoring.Models;
using System;

namespace Refactoring
{
    internal abstract class PerformanceCalculator
    {
        protected readonly Performance _performance;

        protected PerformanceCalculator(Performance aPerformance, Play aPlay)
        {
            _performance = aPerformance;
            Play = aPlay;
        }

        public Play Play { get; }

        public abstract decimal Amount { get; }

        public virtual int VolumeCredits
        {
            get
            {
                var credits = 0;
                credits += Math.Max(_performance.Audience - 30, 0);
                return credits;
            }
        }
    }

    internal class TragedyCalculator : PerformanceCalculator
    {
        public TragedyCalculator(Performance aPerformance, Play aPlay)
            : base(aPerformance, aPlay)
        {
        }

        public override decimal Amount
        {
            get
            {
                var result = 40_000m;
                if (_performance.Audience > 30)
                {
                    result += 1_000 * (_performance.Audience - 30);
                }
                return result;
            } 
        }
    }

    internal class ComedyCalculator : PerformanceCalculator
    {
        public ComedyCalculator(Performance aPerformance, Play aPlay)
            : base(aPerformance, aPlay)
        {
        }

        public override decimal Amount
        {
            get
            {
                var result = 30_000m;
                if (_performance.Audience > 20)
                {
                    result += 10_000 + 500 * (_performance.Audience - 20);
                }
                result += 300 * _performance.Audience;
                return result;
            }
        }

        public override int VolumeCredits => base.VolumeCredits + _performance.Audience / 5;
    }
}
