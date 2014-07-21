using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace HeiMa8.OA.Common
{
    public class Log4NetWriter:ILogWriter
    {
        public void WriteLogInfo(string msg)
        {
            ILog logWrite = log4net.LogManager.GetLogger("Demo");
            logWrite.Error(msg);
        }
    }
}
