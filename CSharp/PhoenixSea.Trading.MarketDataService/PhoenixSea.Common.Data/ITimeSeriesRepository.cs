using System;
using System.Collections.Generic;
using PhoenixSea.Common.Core.MarketData;

namespace PhoenixSea.Common.Data
{
    public interface ITimeSeriesRepository<TEntity> : IDisposable where TEntity : ITimeSeriesEntity
    {
        IEnumerable<TEntity> Get(string nameSpaceKey, DateTime startTimestamp, DateTime endTimestamp, TimeSpan timeSliceLength);
        void Insert(string keyNamespace, IEnumerable<TEntity> timeSeries, TimeSpan timeSlice);
        object ExecuteQuery(string query, params object[] parameters);
        DateTime GetLatestUpdateTimestamp();
    }
}