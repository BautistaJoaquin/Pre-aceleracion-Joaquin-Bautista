using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace preAceleracionDisney.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();
        protected BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TEntity> GetAllEntities()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = _dbContext.Find<TEntity>(id);
            _dbContext.Remove(entity);

            _dbContext.SaveChanges();
        }
    }
}
