using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhoenixSea.Trading.Models.Quandl
{
    public class TreasuryLongTermRateDataSet : QuandlDataSet
    {
        [JsonProperty("data")]
        public List<List<string>> Data { get; set; }
    }
}