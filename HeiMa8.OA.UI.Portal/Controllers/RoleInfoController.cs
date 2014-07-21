using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;

namespace HeiMa8.OA.UI.Portal.Controllers
{
    public class RoleInfoController : BaseController
    {
        short delflagNormal = (short)DelFlagEnum.Normal;
        //
        // GET: /RoleInfo/
        public IRoleInfoBLL RoleInfoBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 添加角色

        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.ModfiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag = delflagNormal;
            RoleInfoBLL.Add(roleInfo);
            return Content("ok");
        }
        #endregion

        #region  批量删除角色
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("对象不能为空");
            }

            List<int> listId = new List<int>();

            string[] IDList = ids.Split(',');

            foreach (var item in IDList)
            {
                listId.Add(int.Parse(item));
            }
            RoleInfoBLL.DeleteList(listId);
            return Content("ok");
        }
        #endregion


        #region 获取所有角色
        public ActionResult GetAllRoleInfos()
        {
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int total = 0;
            //获取分页数据
            short DelFlag = (short)DelFlagEnum.Normal;

            //解决导航属性循环依赖的错误
            var pageData = RoleInfoBLL.GetEntitiesPage(pageSize, pageIndex, out total, u => u.DelFlag == DelFlag, u => u.ID)
                .Select(u => new
                {
                    u.ID,
                   
                    u.ReMark,
                    u.ModfiedOn,
                    u.SubTime,
                    u.RoleName,
                }

                );

            //匿名类
            var data = new { total = total, rows = pageData.ToList() };
            //返回json数据
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改角色
        public ActionResult Edit(int ID)
        {
            ViewData.Model = RoleInfoBLL.GetEntities(u => u.ID == ID).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(RoleInfo roleInfo)
        {
            RoleInfoBLL.Update(roleInfo);
            return Content("ok");
        }
        #endregion
    }
}
