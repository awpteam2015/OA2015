using System.Web;
using NHibernate;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.SessionStorage
{
    public class HttpSessionContainer : ISessionStorageContainer 
    {
        private string _sessionKey = "NHSession";

        public ISession GetCurrentSession()
        {
            ISession nhSession = null;//置空了无需dispose

            if (HttpContext.Current.Items.Contains(_sessionKey))
                nhSession = (ISession)HttpContext.Current.Items[_sessionKey];

            return nhSession;            
        }
        
        public void Store(ISession session)
        {
            if (HttpContext.Current.Items.Contains(_sessionKey))
                HttpContext.Current.Items[_sessionKey] = session;
            else
                HttpContext.Current.Items.Add(_sessionKey, session);
        }

        public void Remove()
        {
            HttpContext.Current.Items.Remove(_sessionKey);
        }
    }
}
