 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;

namespace HeiMa8.OA.BLL
{
   
		
	public partial class ActionInfoBLL:BaseBLL<ActionInfo>,IActionInfoBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.ActionInfoDAL;
        } 
	}
		
	public partial class FileInfoBLL:BaseBLL<FileInfo>,IFileInfoBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.FileInfoDAL;
        } 
	}
		
	public partial class OrderInfoBLL:BaseBLL<OrderInfo>,IOrderInfoBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.OrderInfoDAL;
        } 
	}
		
	public partial class R_UserInfo_ActionInfoBLL:BaseBLL<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.R_UserInfo_ActionInfoDAL;
        } 
	}
		
	public partial class RoleInfoBLL:BaseBLL<RoleInfo>,IRoleInfoBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.RoleInfoDAL;
        } 
	}
		
	public partial class UserInfoBLL:BaseBLL<UserInfo>,IUserInfoBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.UserInfoDAL;
        } 
	}
		
	public partial class UserInfoExtBLL:BaseBLL<UserInfoExt>,IUserInfoExtBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.UserInfoExtDAL;
        } 
	}
		
	public partial class WF_InstanceBLL:BaseBLL<WF_Instance>,IWF_InstanceBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.WF_InstanceDAL;
        } 
	}
		
	public partial class WF_StepBLL:BaseBLL<WF_Step>,IWF_StepBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.WF_StepDAL;
        } 
	}
		
	public partial class WF_TempBLL:BaseBLL<WF_Temp>,IWF_TempBLL //crud
    {
		public override void SetCurrentDAL()
        {
            CurrentDal = this.DbSession.WF_TempDAL;
        } 
	}


}