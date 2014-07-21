 
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
     /// <summary>
    /// 由简单工厂转变成了抽象工厂。
    /// 抽象工厂  VS  简单工厂
    /// 
    /// </summary>
    public partial class StaticDalFactory
    {
   
	

		public static IActionInfoDAL GetActionInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionInfoDAL")
				as IActionInfoDAL;
		}
	

		public static IFileInfoDAL GetFileInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".FileInfoDAL")
				as IFileInfoDAL;
		}
	

		public static IOrderInfoDAL GetOrderInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDAL")
				as IOrderInfoDAL;
		}
	

		public static IR_UserInfo_ActionInfoDAL GetR_UserInfo_ActionInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".R_UserInfo_ActionInfoDAL")
				as IR_UserInfo_ActionInfoDAL;
		}
	

		public static IRoleInfoDAL GetRoleInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".RoleInfoDAL")
				as IRoleInfoDAL;
		}
	

		public static IUserInfoDAL GetUserInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDAL")
				as IUserInfoDAL;
		}
	

		public static IUserInfoExtDAL GetUserInfoExtDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoExtDAL")
				as IUserInfoExtDAL;
		}
	

		public static IWF_InstanceDAL GetWF_InstanceDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_InstanceDAL")
				as IWF_InstanceDAL;
		}
	

		public static IWF_StepDAL GetWF_StepDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_StepDAL")
				as IWF_StepDAL;
		}
	

		public static IWF_TempDAL GetWF_TempDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_TempDAL")
				as IWF_TempDAL;
		}
	}
}