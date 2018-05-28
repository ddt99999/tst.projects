using System;
using Easy.Logger.Interfaces;
using PhoenixSea.Trading.Utils.DI;
using StackExchange.Redis;

namespace PhoenixSea.Trading.NoSql.Redis
{
    public abstract class RedisRepository
    {
        private readonly IEasyLogger _logger = DependencyContainer.Resolve<ILogService>().GetLogger<RedisRepository>();
        public ConnectionMultiplexer Connection { get; protected set; } 
        public string MainKey { get; protected set; }
        public RedisRepository(string connectionString)
        {
            try
            {
                Connection = ConnectionMultiplexer.Connect(connectionString);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }   
        }

        protected abstract string SetMainKey();
        protected abstract IDatabase Database();

        public void Insert(RedisValue value)
        {
            if (!value.HasValue)
                return;

            Database().StringSet(SetMainKey(), value);
        }
    }
}