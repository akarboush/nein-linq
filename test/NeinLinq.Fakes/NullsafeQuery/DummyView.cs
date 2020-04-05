﻿using System.Collections.Generic;

namespace NeinLinq.Fakes.NullsafeQuery
{
    public class DummyView
    {
        public int Year { get; set; }

        public int Numeric { get; set; }

        public bool Question { get; set; }

        public string? FirstWord { get; set; }

        public int CharacterCount { get; set; }

        public IEnumerable<int>? Other { get; set; }

        public IEnumerable<int>? More { get; set; }

        public IEnumerable<int>? Lot { get; set; }
    }
}
