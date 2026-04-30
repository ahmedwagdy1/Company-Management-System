using System.Linq.Expressions;

namespace Application.DAL.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> predicate);
        Task<TEntity?> GetByIdAsync(int id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
