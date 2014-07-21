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
    public partial class DbSession:IDbSession
    {
        #region 由T4模板自动生成 DbSession.tt
        
        //public IUserInfoDAL UserInfoDAL
        //{
        //    get { return StaticDalFactory.GetUserInfoDal(); }
        //}

        //public IOrderInfoDAL OrderInfoDAL
        //{
        //    get { return StaticDalFactory.GetOrderInfoDal(); }
        //}
        #endregion
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}
