using log4net.Layout;

namespace SyncSoft.ROM.Infrastructure.Logging.CustomLayout
{
    public class MyPatternLayout : PatternLayout
    {
        public MyPatternLayout()
        {
            this.AddConverter("property", typeof(MyPatternLayoutConverter));
        }
    }
}
