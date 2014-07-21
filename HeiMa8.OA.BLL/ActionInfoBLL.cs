                     using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;

namespace HeiMa8.OA.BLL
{
    public partial class ActionInfoBLL:BaseBLL<ActionInfo>,IActionInfoBLL
    {

        public bool SetRole(int userId, List<int> roleIds)
        {

            //原来的全部剁掉 添加新的
            //获得当前权限
            var actionInfo = DbSession.ActionInfoDAL.GetEntities(u => u.ID == userId).FirstOrDefault();
            actionInfo.RoleInfo.Clear();//全剁掉

            //获得当前roleIds中的所有角色
            var allRoles = DbSession.RoleInfoDAL.GetEntities(u => roleIds.Contains(u.ID));
            foreach (var roleInfo in allRoles)
            {
                actionInfo.RoleInfo.Add(roleInfo);//添加新的角色
            }
            DbSession.SaveChanges();
            return true;
        }
    }
}
