using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Log4Net
{
    class Program
    {
        static void Main(string[] args)
        {
            //从配置文件读取log4net的配置。，然后进行初始化工作。

            log4net.Config.XmlConfigurator.Configure();

            //写日志

            ILog logWriter = log4net.LogManager.GetLogger("DemoLogWriter");

            logWriter.Debug("sssssssss调试信息");

            logWriter.Error("dfhg小级别的错误");
        }
    }
}
