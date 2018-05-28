using System;
using FileHelpers;

namespace PhoenixSea.Trading.Models.Quandl
{
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    public class TimeSeries
    {
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime Timestamp;
    }
}