using System.Linq;

namespace Project.Infrastructure.FrameworkCore.Domain.Repositories.Interface
{
    internal interface IRepositoryBase<TEntity, TKey>   where TEntity : class
    {

        TKey Save(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void SaveOrUpdate(TEntity entity);

    }
}
