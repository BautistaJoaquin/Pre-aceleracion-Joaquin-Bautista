using System.Collections.Generic;

namespace preAceleracionDisney.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Delete(int id);
        TEntity Get(int id);
        List<TEntity> GetAllEntities();
        TEntity Update(TEntity entity);
    }
}