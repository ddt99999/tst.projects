using System;

namespace PhoenixSea.Trading.Models.Quandl
{
    public class TreasuryLongTermRateData : TimeSeriesData
    {
        public double LongTermCompositeMoreThan10Years { get; set; }
        public double Treasury20YearsCmt { get; set; }
        public double ExtrapolationFactor { get; set; }
    }
}