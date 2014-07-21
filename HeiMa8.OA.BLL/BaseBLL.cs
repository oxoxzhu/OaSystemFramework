using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HeiMa8.OA.DALFactory;
using HeiMa8.OA.IDAL;

namespace HeiMa8.OA.BLL
{
    /// <summary>
    /// 父类要逼迫子类给父类的一个属性赋值。
    /// 赋值的操作必须在父类的方法调用之前先执行。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseBLL<T> where T:class,new ()
    {

        public IBaseDAL<T> CurrentDal { get; set; }
        public IDbSession DbSession
        {
            get
            {
                return DbSessionFactory.GetCurrentDbSession();
            }
        }
        public BaseBLL()
        {
            SetCurrentDAL();
        }
        public abstract void SetCurrentDAL();//抽象方法：要求子类必须实现。

        //增删查改
        #region 查询

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> lmbdaWhere)
        {
            return CurrentDal.GetEntities(lmbdaWhere);

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
        public IQueryable<T> GetEntitiesPage<S>(int PageSize, int PageIndex, out int total, Expression<Func<T, bool>> lmbdaWhere, Expression<Func<T, S>> OrderByLmbad)
        {
            return CurrentDal.GetEntitiesPage(PageSize, PageIndex, out total, lmbdaWhere, OrderByLmbad);
        }
        #region cud
        public bool Add(T entity)
        {
            CurrentDal.Add(entity);
            return DbSession.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {

            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;

        }

        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges() > 0;
        }

        public bool DeleteList(List<int> idList)
        {
            CurrentDal.DeleteList(idList);
            return DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
