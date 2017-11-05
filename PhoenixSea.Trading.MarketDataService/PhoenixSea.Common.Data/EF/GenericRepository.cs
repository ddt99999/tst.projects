using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PhoenixSea.Common.Data.EF
{
    public class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : EntityBase 
    {
        protected readonly IAmbientDbContextLocator AmbientDbContextLocator;

        protected TDbContext DbContext
        {
            get
            {
                var dbContext = AmbientDbContextLocator.Get<TDbContext>();

                if (dbContext == null)
                    throw new InvalidOperationException($"No ambient DbContext of type {nameof(TDbContext)} found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }

        public GenericRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null) throw new ArgumentNullException(nameof(ambientDbContextLocator));
            AmbientDbContextLocator = ambientDbContextLocator;
        }

        public void Insert(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            //DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            
            //DbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            //DbContext.SaveChanges();
        }

        public IEnumerable<TEntity> FindAll()
        {
            return DbContext.Set<TEntity>();
        }

        public TEntity FindById(long id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> FindByIdAsync(long id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
    }
}