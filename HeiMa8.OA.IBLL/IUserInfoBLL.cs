using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Param;

namespace HeiMa8.OA.IBLL
{
   public partial interface IUserInfoBLL:IBaseBLL<UserInfo>
    {
       IQueryable<UserInfo> LoadDataByParam(QueryParam userParam);

       bool SetRole(int userId, List<int> roleIds);
    }
}
