using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace PhoenixSea.Trading.NoSql.Redis
{
    public class UsTreasuryRepository : RedisRepository, IUsTreasuryRepository
    {
        public UsTreasuryRepository(string connectionString) : base(connectionString)
        {
        }

        protected override string SetMainKey()
        {
            return "FIXED_INCOME:US_TREASURY";
        }

        protected override IDatabase Database()
        {
            return Connection.GetDatabase(0);
        }
    }
}
