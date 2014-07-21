using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;

namespace HeiMa8.OA.EFDAL
{
    /// <summary>
    /// 职责：封装所有的Dal的公共crud方法
    /// 类的职责一定要单一
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDAL<T> where T:class,new ()
    {
        //依赖抽象编程。好处：可以应对变化的时候，改变最小。
        public DbContext dbcontext
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }
        //增删查改
        #region 查询

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> lmbdaWhere)
        {
            return dbcontext.Set<T>().Where(lmbdaWhere).AsQueryable<T>();

        }
        #endregion
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="total"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public IQueryable<T> GetEntitiesPage<S>(int PageSize,int PageIndex,out int total,Expression<Func<T, bool>> lmbdaWhere,Expression<Func<T, S>> OrderByLmbad)
        {
            total = dbcontext.Set<T>().Where(lmbdaWhere).Count();
            var tmp = dbcontext.Set<T>().Where(lmbdaWhere).OrderBy<T,S>(OrderByLmbad).
                Skip(PageSize * (PageIndex - 1)).
                Take(PageSize);
            return tmp;
        }
        #region cud
        public bool Add(T entity)
        {
            dbcontext.Set<T>().Add(entity);
            return dbcontext.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {

            dbcontext.Entry(entity).State = EntityState.Modified;
            return dbcontext.SaveChanges() > 0;

        }

        public bool Delete(int id)
        {
            T entity = dbcontext.Set<T>().Find(id);
            dbcontext.Entry(entity).State = EntityState.Deleted;
            return dbcontext.SaveChanges() > 0;
        }

        public bool DeleteList(List<int> idList)
        {
            foreach (var item in idList)
            {
                  T entity = dbcontext.Set<T>().Find(item);
                  dbcontext.Entry(entity).Property("DelFlag").CurrentValue = (short)DelFlagEnum.Deleted;
                  dbcontext.Entry(entity).Property("DelFlag").IsModified = true;
            }
            return dbcontext.SaveChanges() > 0;
        }
        #endregion

    }
}
