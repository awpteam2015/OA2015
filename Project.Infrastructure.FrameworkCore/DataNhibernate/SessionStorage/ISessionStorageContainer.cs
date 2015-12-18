using NHibernate;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.SessionStorage
{
    public interface ISessionStorageContainer
    {
        ISession GetCurrentSession();
        void Store(ISession session);
        void Remove();
    }
}
