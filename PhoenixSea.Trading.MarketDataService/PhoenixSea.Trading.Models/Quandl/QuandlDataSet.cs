using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhoenixSea.Trading.Models.Quandl
{
    public class QuandlDataSet
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("dataset_code")]
        public string DatasetCode { get; set; }
        [JsonProperty("database_code")]
        public string DatabaseCode { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("refreshed_at")]
        public DateTime RefreshedAt { get; set; }
        [JsonProperty("newest_available_date")]
        public DateTime NewestAvailableDate { get; set; }
        [JsonProperty("oldest_available_date")]
        public DateTime OldestAvailableDate { get; set; }
        [JsonProperty("column_names")]
        public List<string> ColumnNames { get; set; }
        [JsonProperty("frequency")]
        public string Frequency { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("premium")]
        public bool IsPremium { get; set; }
        [JsonProperty("limit")]
        public string Limit { get; set; }
        [JsonProperty("transform")]
        public string Transform { get; set; }
        [JsonProperty("column_index")]
        public List<long> ColumnIndex { get; set; }
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }
        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }
        [JsonProperty("collapse")]
        public string Collapse { get; set; }
        [JsonProperty("order")]
        public string Order { get; set; }
        [JsonProperty("database_id")]
        public long DatabaseId { get; set; }
    }
}