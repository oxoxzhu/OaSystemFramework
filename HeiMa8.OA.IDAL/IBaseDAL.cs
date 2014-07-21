using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HeiMa8.OA.IDAL
{
    public interface IBaseDAL <T> where T:class,new ()
    {
        //增删查改
        #region 查询

        IQueryable<T> GetEntities(Expression<Func<T, bool>> lmbdaWhere);
        #endregion

        IQueryable<T> GetEntitiesPage<S>(int PageSize, int PageIndex, out int total, Expression<Func<T, bool>> lmbdaWhere, Expression<Func<T, S>> OrderByLmbad);
        #region cud
        bool Add(T entity);

        bool Update(T entity);

        bool Delete(int id);
        bool DeleteList(List<int> idList);
        #endregion

    }
}
