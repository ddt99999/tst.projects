using System;
using FileHelpers;

namespace PhoenixSea.Trading.Models.Yahoo
{
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    public class TimeSeries
    {
        [FieldConverter(ConverterKind.Date, "dd-MM-yy")]
        public DateTime Timestamp;
    }
}