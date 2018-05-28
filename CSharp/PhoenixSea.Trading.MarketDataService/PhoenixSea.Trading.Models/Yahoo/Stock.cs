using FileHelpers;

namespace PhoenixSea.Trading.Models.Yahoo
{
    [IgnoreFirst(1)]
    [DelimitedRecord(",")]
    public class Stock : TimeSeries
    {
        [FieldConverter(ConverterKind.Double, ".")]
        public double High;

        [FieldConverter(ConverterKind.Double, ".")]
        public double Low;

        [FieldConverter(ConverterKind.Double, ".")]
        public double Open;

        [FieldConverter(ConverterKind.Double, ".")]
        public double Close;

        [FieldConverter(ConverterKind.Double, ".")]
        public double AdjustedClose;

        [FieldConverter(ConverterKind.Int64)]
        public long Volume;
    }
}