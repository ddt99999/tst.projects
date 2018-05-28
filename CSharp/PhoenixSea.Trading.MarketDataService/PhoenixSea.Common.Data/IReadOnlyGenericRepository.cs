using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoenixSea.Common.Data
{
    public interface IReadOnlyGenericRepository<TEntity> where TEntity : EntityBase
    {
        IEnumerable<TEntity> FindAll();
        TEntity FindById(long id);
        Task<TEntity> FindByIdAsync(long id);
    }
}