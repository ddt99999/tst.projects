namespace PhoenixSea.Common.Data
{
    public interface IGenericRepository<TEntity> : IReadOnlyGenericRepository<TEntity> where TEntity : EntityBase
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}