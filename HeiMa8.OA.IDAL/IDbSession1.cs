 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.Model;

namespace HeiMa8.OA.IDAL
{
    public partial interface IDbSession
    {
   
	 
		IActionInfoDAL ActionInfoDAL { get;}
	 
		IFileInfoDAL FileInfoDAL { get;}
	 
		IOrderInfoDAL OrderInfoDAL { get;}
	 
		IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL { get;}
	 
		IRoleInfoDAL RoleInfoDAL { get;}
	 
		IUserInfoDAL UserInfoDAL { get;}
	 
		IUserInfoExtDAL UserInfoExtDAL { get;}
	 
		IWF_InstanceDAL WF_InstanceDAL { get;}
	 
		IWF_StepDAL WF_StepDAL { get;}
	 
		IWF_TempDAL WF_TempDAL { get;}
	}

}