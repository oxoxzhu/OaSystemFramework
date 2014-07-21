 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.Model;

namespace HeiMa8.OA.IDAL
{
   
		
	
	public partial interface IActionInfoDAL : IBaseDAL<ActionInfo>
    {
	}   
		
	
	public partial interface IFileInfoDAL : IBaseDAL<FileInfo>
    {
	}   
		
	
	public partial interface IOrderInfoDAL : IBaseDAL<OrderInfo>
    {
	}   
		
	
	public partial interface IR_UserInfo_ActionInfoDAL : IBaseDAL<R_UserInfo_ActionInfo>
    {
	}   
		
	
	public partial interface IRoleInfoDAL : IBaseDAL<RoleInfo>
    {
	}   
		
	
	public partial interface IUserInfoDAL : IBaseDAL<UserInfo>
    {
	}   
		
	
	public partial interface IUserInfoExtDAL : IBaseDAL<UserInfoExt>
    {
	}   
		
	
	public partial interface IWF_InstanceDAL : IBaseDAL<WF_Instance>
    {
	}   
		
	
	public partial interface IWF_StepDAL : IBaseDAL<WF_Step>
    {
	}   
		
	
	public partial interface IWF_TempDAL : IBaseDAL<WF_Temp>
    {
	}   


}