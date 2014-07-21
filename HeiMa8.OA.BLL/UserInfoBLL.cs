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
    public partial class UserInfoBLL:BaseBLL<UserInfo>,IUserInfoBLL
    {
        #region 模板自动生成
        
        //public override void SetCurrentDAL()
        //{
        //    CurrentDal = this.DbSession.UserInfoDAL;
        //}
        #endregion
        #region 多条件查询
        public IQueryable<UserInfo> LoadDataByParam(Model.Param.QueryParam userQueryParam)
        {
            short normalFlag=(short)DelFlagEnum.Normal;
            var temp = CurrentDal.GetEntities(u => u.DelFlag == normalFlag);

            //过滤
            if (!string.IsNullOrEmpty(userQueryParam.SearchName))
            {
                temp = temp.Where(u => u.UName.Contains(userQueryParam.SearchName)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(userQueryParam.SearchReMark))
            {
                temp = temp.Where(u => u.ReMark.Contains(userQueryParam.SearchReMark)).AsQueryable();
            }

            userQueryParam.TotalCount = temp.Count();

            return temp.OrderBy(u => u.ID)
                .Skip((userQueryParam.pageIndex - 1) * userQueryParam.pageSize)
                .Take(userQueryParam.pageSize).AsQueryable();
        }
        #endregion


        public bool SetRole(int userId, List<int> roleIds)
        {
            //原来的全部剁掉 添加新的
            //获得当前用户
            var user = DbSession.UserInfoDAL.GetEntities(u => u.ID == userId).FirstOrDefault();
            user.RoleInfo.Clear();//全剁掉

            //获得当前roleIds中的所有角色
            var allRoles = DbSession.RoleInfoDAL.GetEntities(u => roleIds.Contains(u.ID));
            foreach (var roleInfo in allRoles) 
            {
                user.RoleInfo.Add(roleInfo);//添加新的角色
            }
            DbSession.SaveChanges();
            return true;

        }
    }
}
