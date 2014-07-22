using System;
using System.Linq;
using HeiMa8.OA.EFDAL;
using HeiMa8.OA.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeiMa8.OA.UnitTest.DAL
{
    [TestClass]
    public class UserInfoDALTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //单元测试是没用的,浪费开发时间而已？
            //1.节省的改bug时间
            //2.对项目非常有自信
            //3.单元测试也是一种设计 (该方法是什么目的)
            //4. 它也是一种项目管理的手段. TDD:测试驱动开发
            //UserInfoDAL dal = new UserInfoDAL();

            //for (int i = 0; i < 10; i++)
            //{
            //    dal.Add(new Model.UserInfo() { UName = i+"ssssss" });
            //}

            //IQueryable<UserInfo> temp = dal.GetEntities(u => u.UName.Contains("ss"));

            ////断言
            //Assert.AreEqual(true, temp.Count() >= 10);
        }
    }
}
