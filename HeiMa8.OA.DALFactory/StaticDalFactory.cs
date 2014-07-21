using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.IDAL;

namespace HeiMa8.OA.DALFactory
{
    public partial class StaticDalFactory
    {
        public static string assemblyName = ConfigurationManager.AppSettings["DalAssemblyName"];
        #region 由T4模板自动生成
        //public static IUserInfoDAL GetUserInfoDal()
        //{
        //    //HttpRuntime.Cache.Get("")
        //    //return new NhUserInfoDal();
        //    //把上面的new去掉：希望改一个配置那么创建实例就发生变化，不需要改代码。
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDAL")
        //           as IUserInfoDAL;
        //}

        //public static IOrderInfoDAL GetOrderInfoDal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDAL")
        //        as IOrderInfoDAL;
        //} 
        #endregion
    }
}
