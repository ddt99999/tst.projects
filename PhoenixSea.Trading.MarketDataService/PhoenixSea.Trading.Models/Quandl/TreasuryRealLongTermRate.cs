using System;
using FileHelpers;

namespace PhoenixSea.Trading.Models.Quandl
{
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    public class TreasuryRealLongTermRate : TimeSeries
    {
        [FieldConverter(ConverterKind.Double, ".")]
        public double LongTermRealAverageRate;
    }
}