using System.Web;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.SessionStorage
{
    public static class SessionStorageFactory
    {
        public static ISessionStorageContainer _nhSessionStorageContainer;

        public static ISessionStorageContainer _nhSessionStorageContainerThread;

        public static ISessionStorageContainer GetStorageContainer()
        {
            if (HttpContext.Current == null)
            {
                if (_nhSessionStorageContainer != null)
                {
                    return _nhSessionStorageContainer;
                }
                else
                {
                    _nhSessionStorageContainer = new ThreadSessionStorageContainer();
                    return _nhSessionStorageContainer;
                }
            }
            else
            {

                if (_nhSessionStorageContainerThread != null)
                {
                    return _nhSessionStorageContainerThread;
                }
                else
                {
                    _nhSessionStorageContainerThread = new HttpSessionContainer();
                    return _nhSessionStorageContainerThread;
                }
            }
        }

        public static void Dispose()
        {
            if (_nhSessionStorageContainer != null)
            {
                _nhSessionStorageContainer.Remove();
                //_nhSessionStorageContainer.GetCurrentSession().Dispose();
                _nhSessionStorageContainer = null;
            }

            if (_nhSessionStorageContainerThread != null)
            {
                _nhSessionStorageContainerThread.Remove();
                // _nhSessionStorageContainerThread.GetCurrentSession().Dispose();
                _nhSessionStorageContainerThread = null;
            }
        }


    }
}
