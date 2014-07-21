 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.IDAL;
using HeiMa8.OA.Model;

namespace HeiMa8.OA.EFDAL
{
   
		
	public partial class ActionInfoDAL : BaseDAL<ActionInfo>,IActionInfoDAL
    {
	}   
		
	public partial class FileInfoDAL : BaseDAL<FileInfo>,IFileInfoDAL
    {
	}   
		
	public partial class OrderInfoDAL : BaseDAL<OrderInfo>,IOrderInfoDAL
    {
	}   
		
	public partial class R_UserInfo_ActionInfoDAL : BaseDAL<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDAL
    {
	}   
		
	public partial class RoleInfoDAL : BaseDAL<RoleInfo>,IRoleInfoDAL
    {
	}   
		
	public partial class UserInfoDAL : BaseDAL<UserInfo>,IUserInfoDAL
    {
	}   
		
	public partial class UserInfoExtDAL : BaseDAL<UserInfoExt>,IUserInfoExtDAL
    {
	}   
		
	public partial class WF_InstanceDAL : BaseDAL<WF_Instance>,IWF_InstanceDAL
    {
	}   
		
	public partial class WF_StepDAL : BaseDAL<WF_Step>,IWF_StepDAL
    {
	}   
		
	public partial class WF_TempDAL : BaseDAL<WF_Temp>,IWF_TempDAL
    {
	}   

}