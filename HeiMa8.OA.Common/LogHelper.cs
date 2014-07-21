using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeiMa8.OA.Common
{
    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        public static List<ILogWriter> LogWriterList = new List<ILogWriter>();

        //public static WriteLogDel WriteLogDelFunc;
        static LogHelper()
        {
            //WriteLogDelFunc = new WriteLogDel(WriteLogToFile);
            //WriteLogDelFunc += WriteLogToMongodb;

            //LogWriterList.Add(new TextFileWrieter());
            //LogWriterList.Add(new SqlServerWriter());
            LogWriterList.Add(new Log4NetWriter());

            //把从队列中获取错误消息写到 日志文件里面去。
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    lock (ExceptionStringQueue)
                    {
                        if (ExceptionStringQueue.Count > 0)
                        {
                            string str = ExceptionStringQueue.Dequeue();
                            //把异常信息 写到  日志文件里面去。
                            //变化点：有可能写到日志文件，有可能写到数据库里面去。有可能两个地方都写。

                            //执行委托方法。把异常信息写到委托里面去。
                            //WriteLogDelFunc(str);

                            //ILogWriter writer =new TextFileWrieter();
                            //writer.WriteLogInfo(str);
                            foreach (var logWriter in LogWriterList)
                            {
                                logWriter.WriteLogInfo(str);
                            }

                            //log4net:已经帮助我们处理好了观察者模式。
                        }
                        else
                        {
                            Thread.Sleep(30);
                        }
                    }
                }
            });
        }

        public static void WriteLog(string exceptionText)
        {
            lock (ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(exceptionText);
            }
        }
    }
}
