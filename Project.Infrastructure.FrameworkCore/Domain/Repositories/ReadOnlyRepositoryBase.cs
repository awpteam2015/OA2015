using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Domain.Repositories.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Repositories
{
    public class ReadOnlyRepositoryBase<TEntity, TKey> : IReadOnlyRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        public IQueryable<TEntity> Query()
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            return session.Query<TEntity>();
        }

        public TEntity GetById(TKey id)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            return session.Get<TEntity>(id);
        }

        public void Evict(TEntity entity)
        {
            SessionFactoryManager.GetCurrentSession().Evict(entity);
        }

        public void Evict(IEnumerable<TEntity> list)
        {
            foreach (var p in list)
            {
                SessionFactoryManager.GetCurrentSession().Evict(p);
            }
        }
    }
}
