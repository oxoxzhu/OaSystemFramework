using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeiMa8.OA.IDAL
{
    public partial interface IDbSession
    {
        #region 由T4模板自动生成 IDbSession.tt
        
        //IUserInfoDAL UserInfoDAL { get; }
        //IOrderInfoDAL OrderInfoDAL { get; }
        #endregion

        int SaveChanges();
    }
}
