using System.Collections.Generic;

namespace HistogramBuilder.Domain.Contract
{
    public class Histogram
    {
        public Histogram(IDictionary<byte, int> tonalValueCounts)
        {
            TonalValueCounts = tonalValueCounts;
        }

        public IDictionary<byte, int> TonalValueCounts { get; }
    }
}