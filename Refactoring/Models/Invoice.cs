﻿using System.Collections.Generic;

namespace Refactoring.Models
{
    public class Invoice
    {
        public string Customer { get; }
        public IList<Performance> Performances { get; }

        public Invoice(string customer)
        {
            Customer = customer;
            Performances = new List<Performance>();
        }

        public void Add(string playId, int audience)
        {
            Performances.Add(new Performance(playId, audience));
        }

        public class Performance
        {
            public string PlayId { get; }
            public int Audience { get; }

            public Performance(string playId, int audience)
            {
                PlayId = playId;
                Audience = audience;
            }
        }
    }
}
