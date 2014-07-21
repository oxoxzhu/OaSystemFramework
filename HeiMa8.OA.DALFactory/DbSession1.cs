 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.EFDAL;
using HeiMa8.OA.IDAL;

namespace HeiMa8.OA.DALFactory
{
    public partial class DbSession :IDbSession
    {
   
		
	public IActionInfoDAL ActionInfoDAL
    {
        get { return StaticDalFactory.GetActionInfoDal(); }
    }
		
	public IFileInfoDAL FileInfoDAL
    {
        get { return StaticDalFactory.GetFileInfoDal(); }
    }
		
	public IOrderInfoDAL OrderInfoDAL
    {
        get { return StaticDalFactory.GetOrderInfoDal(); }
    }
		
	public IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL
    {
        get { return StaticDalFactory.GetR_UserInfo_ActionInfoDal(); }
    }
		
	public IRoleInfoDAL RoleInfoDAL
    {
        get { return StaticDalFactory.GetRoleInfoDal(); }
    }
		
	public IUserInfoDAL UserInfoDAL
    {
        get { return StaticDalFactory.GetUserInfoDal(); }
    }
		
	public IUserInfoExtDAL UserInfoExtDAL
    {
        get { return StaticDalFactory.GetUserInfoExtDal(); }
    }
		
	public IWF_InstanceDAL WF_InstanceDAL
    {
        get { return StaticDalFactory.GetWF_InstanceDal(); }
    }
		
	public IWF_StepDAL WF_StepDAL
    {
        get { return StaticDalFactory.GetWF_StepDal(); }
    }
		
	public IWF_TempDAL WF_TempDAL
    {
        get { return StaticDalFactory.GetWF_TempDal(); }
    }
	}
}