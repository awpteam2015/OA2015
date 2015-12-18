using System;
using System.Collections;
using System.Threading;
using NHibernate;
using NHibernate.Mapping.ByCode;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.SessionStorage
{
    public class ThreadSessionStorageContainer : ISessionStorageContainer
    {
        private static readonly Hashtable _nhSessions = new Hashtable();

        public ISession GetCurrentSession()
        {
            ISession nhSession = null;

            if (_nhSessions.Contains(GetThreadName()))
                nhSession = (ISession)_nhSessions[GetThreadName()];

            return nhSession;
        }

        public void Store(ISession session)
        {
            if (_nhSessions.Contains(GetThreadName()))
                _nhSessions[GetThreadName()] = session;
            else
                _nhSessions.Add(GetThreadName(), session);
        }

        public void Remove()
        {
            _nhSessions.Remove(GetThreadName());
        }

        private static string GetThreadName()
        {
            if (Thread.CurrentThread.Name==null)
            {
                return Guid.NewGuid().ToString();
            }

            return Thread.CurrentThread.Name;
        }
    }
}
