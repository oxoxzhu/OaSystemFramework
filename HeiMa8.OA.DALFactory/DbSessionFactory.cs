using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.IDAL;

namespace HeiMa8.OA.DALFactory
{
    public class DbSessionFactory
    {
        /// <summary>
        /// 一次请求公用一个Dbsession实例
        /// </summary>
        /// <returns></returns>
        public static IDbSession GetCurrentDbSession()
        {
            IDbSession db = CallContext.GetData("DbSession") as IDbSession;
            if (db == null)
            {
                db = new DbSession();
                CallContext.SetData("DbSession", db);
            }
            return db;
        }
    }
}
