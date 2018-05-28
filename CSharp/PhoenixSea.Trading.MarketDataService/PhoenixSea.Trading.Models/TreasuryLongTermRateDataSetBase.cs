using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoenixSea.Trading.Models.Quandl;

namespace PhoenixSea.Trading.Models
{
    public class TreasuryLongTermRateDataSetBase : IQuandlData<TreasuryLongTermRateDataSet>
    {
        [JsonProperty("dataset")]
        public TreasuryLongTermRateDataSet DataSet { get; set; }
    }
}
