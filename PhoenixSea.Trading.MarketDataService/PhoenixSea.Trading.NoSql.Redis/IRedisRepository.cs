using StackExchange.Redis;

namespace PhoenixSea.Trading.NoSql.Redis
{
    public interface IRedisRepository
    {
        void Insert(RedisValue value);
    }
}