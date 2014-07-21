using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeiMa8.OA.Common
{
    public class SqlServerWriter:ILogWriter
    {
        public void WriteLogInfo(string msg)
        {
            Console.WriteLine("写到数据库中"+msg);
        }
    }
}
