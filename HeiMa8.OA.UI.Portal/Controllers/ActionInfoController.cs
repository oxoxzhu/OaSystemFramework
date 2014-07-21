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
    public class ActionInfoController : BaseController
    {
        short delflagNormal = (short)DelFlagEnum.Normal;
        //
        // GET: /ActionInfo/
        public IActionInfoBLL ActionInfoBLL { get; set; }
        public IRoleInfoBLL RoleInfoBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 添加权限

        public ActionResult Add(ActionInfo actionInfo)
        {
            actionInfo.ModfiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.DelFlag = delflagNormal;
            actionInfo.IsMenu = Request["IsMenu"] !=null ? true : false;
            ActionInfoBLL.Add(actionInfo);
            return Content("ok");
        }
        #endregion

        #region  批量删除权限
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
            ActionInfoBLL.DeleteList(listId);
            return Content("ok"); 
        }
        #endregion


        #region 获取所有用户
        public ActionResult GetAllActionInfos()
        {
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int total = 0;
            //string SearchName = Request["SearchName"];
            //string SearchReMark = Request["SearchMark"];


            //获取分页数据
            short DelFlag = (short)DelFlagEnum.Normal;
           
            //解决导航属性循环依赖的错误
            var pageData = ActionInfoBLL.GetEntitiesPage(pageSize, pageIndex, out total, u => u.DelFlag == DelFlag, u => u.ID)
                .Select(u => new 
                     {
                    u.ID, u.IsMenu,u.Url,u.ReMark,u.HttpMethod, u.ModfiedOn, u.SubTime,u.ActionName,u.MenuIcon 
                    }
                    
                );

            //匿名类
            var data = new { total = total, rows = pageData.ToList() };
            //返回json数据
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改用户
        public ActionResult Edit(int ID)
        {
            ViewData.Model = ActionInfoBLL.GetEntities(u => u.ID == ID).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {
            ActionInfoBLL.Update(actionInfo);
            return Content("ok");
        }
        #endregion

        #region 上传图片处理
        public ActionResult UploadImage()
        {

            var file = Request.Files["fileMenuIcon"];

            string path = "/UploadFiles/UploadImg/" + Guid.NewGuid().ToString() + "-" + file.FileName;

            file.SaveAs(Request.MapPath(path));

            return Content(path);
        }
        #endregion

        #region 设置角色
        public ActionResult SetRole(int ID)
        {
            //获取当前用户
            ActionInfo actionInfo = ActionInfoBLL.GetEntities(u => u.ID == ID).FirstOrDefault();

            //吧所有角色发送到前台

            ViewBag.AllRoles = RoleInfoBLL.GetEntities(u => u.DelFlag == delflagNormal).ToList();

            //用户已经管理的角色送到前台

            ViewBag.ExistRoles = (from r in actionInfo.RoleInfo
                                  select r.ID
                                    ).ToList();
            return View(actionInfo);

        }

        #endregion

        #region 处理角色
        public ActionResult ProessSetRole(int Uid)
        {
            //当前用户id --uid

            //获得所有被选中的角色
            List<int> setRoleList = new List<int>();
            //遍历表单中的所有标签
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleList.Add(roleId);
                }

            }
            ActionInfoBLL.SetRole(Uid, setRoleList);
            return Content("ok");
        }
        #endregion
    }
}
