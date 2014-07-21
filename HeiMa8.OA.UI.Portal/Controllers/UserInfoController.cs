using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.BLL;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;
using HeiMa8.OA.Model.Param;

namespace HeiMa8.OA.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        //Spring.NET 注入
        short DelFlag = (short)DelFlagEnum.Normal;
        public IUserInfoBLL UserInfoBLL { get; set; }// = new UserInfoBLL();
        public IRoleInfoBLL RoleInfoBLL { get; set; }
        public IActionInfoBLL ActionInfoBLL { get; set; }
        public IR_UserInfo_ActionInfoBLL R_UserInfo_ActionInfoBLL{ get; set; }
        public ActionResult Index()
        {

            //throw new Exception("顶顶顶顶顶顶");
            ViewData.Model = UserInfoBLL.GetEntities(u => true);
            return View();
        }

        #region 添加用户
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.SubTime = DateTime.Now;
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.DelFlag = (short)DelFlagEnum.Normal;
            UserInfoBLL.Add(userInfo);
            return Content("ok");
        }
        #endregion

        #region 修改用户
        public ActionResult Edit(int ID)
        {
            ViewData.Model=UserInfoBLL.GetEntities(u => u.ID == ID).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo) 
        {
            UserInfoBLL.Update(userInfo);
            return Content("ok");
        }
        #endregion

        #region 批量删除

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids)) {
                return Content("对象不能为空");
            }

            List<int> listId = new List<int>();

            string[] IDList = ids.Split(',');

            foreach (var item in IDList)
            {
                listId.Add(int.Parse(item));
            }

            //UserInfoBLL.Delete();
            UserInfoBLL.DeleteList(listId);

            return Content("ok");
        }

        #endregion

        #region 获取所有用户
        public ActionResult GetAllUserInfos()
        {
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int total = 0;
            string SearchName = Request["SearchName"];
            string SearchReMark = Request["SearchMark"];


            //获取分页数据
             
              var queryParam = new QueryParam() { pageSize = pageSize, pageIndex = pageIndex, TotalCount = 0, SearchName = SearchName, SearchReMark = SearchReMark};

             var pageData = UserInfoBLL.LoadDataByParam(queryParam).Select(u => new { ID=u.ID, u.UName, u.ReMark, u.Pwd, u.ShowName, u.ModfiedOn, u.SubTime });
            //解决导航属性循环依赖的错误
             // var pageData = UserInfoBLL.GetEntitiesPage(pageSize, pageIndex, out total, u => u.DelFlag == DelFlag, u => u.ID).Select(u => new { u.ID, u.UName, u.ReMark, u.Pwd, u.ShowName, u.ModfiedOn, u.SubTime });

            //匿名类
              var data = new { total =queryParam.TotalCount, rows = pageData.ToList() };
            //返回json数据
              return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 设置角色
        public ActionResult SetRole(int ID)
        { 
            //获取当前用户
            UserInfo user = UserInfoBLL.GetEntities(u => u.ID == ID).FirstOrDefault();

            //吧所有角色发送到前台

            ViewBag.AllRoles = RoleInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();

            //用户已经管理的角色送到前台

            ViewBag.ExistRoles = (from r in user.RoleInfo
                                  select r.ID
                                    ).ToList();
            return View(user);
        
        }

        #endregion

        #region 处理角色
        public ActionResult ProessSetRole(int Uid)
        {
            //当前用户id --uid

            //获得所有被选中的角色
            List<int> setRoleList = new List<int>();
            //遍历表单中的所有标签
            foreach(var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleList.Add(roleId);
                }

            }
            UserInfoBLL.SetRole(Uid, setRoleList);
            return Content("ok");
        }
        #endregion

        #region 设置特殊权限

        public ActionResult SetAction(int ID)
        {
            //获取当前用户
            UserInfo user = UserInfoBLL.GetEntities(u => u.ID == ID).FirstOrDefault();
            //吧所有权限发送到前台
            ViewBag.User=user;
            ViewBag.AllActions = ActionInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();
            ViewBag.AllExistActions = user.R_UserInfo_ActionInfo.Where(u => u.DelFlag == DelFlag
            ).ToList();
                   
            return View();
        }

        //UId: UId, ActionId: ActionId, Value: value
        //设置当前用户的特殊权限
        public ActionResult ProcessSetUserAction(int UId,int ActionId,int Value)
        {
            var ResuserAction = R_UserInfo_ActionInfoBLL.GetEntities(u => u.ActionInfoID == ActionId && u.UserInfoID == UId && u.DelFlag == DelFlag).FirstOrDefault();

            //拒绝
            if (ResuserAction != null)
            {
                ResuserAction.HasPermission = Value == 1 ? true : false;
                R_UserInfo_ActionInfoBLL.Update(ResuserAction);
            }
            else
            {
                R_UserInfo_ActionInfo rUserInfoActionInfo = new R_UserInfo_ActionInfo();
                rUserInfoActionInfo.HasPermission = Value == 1 ? true : false;
                rUserInfoActionInfo.UserInfoID = UId;
                rUserInfoActionInfo.ActionInfoID = ActionId;
                rUserInfoActionInfo.DelFlag = DelFlag;
                R_UserInfo_ActionInfoBLL.Add(rUserInfoActionInfo);
            }
            return Content("ok");
        }
        #endregion

        #region 删除特殊权限
        public ActionResult DeleteUserAction(int UId, int ActionId)
        {
            var ResuserAction = R_UserInfo_ActionInfoBLL.GetEntities(u => u.ActionInfoID == ActionId && u.UserInfoID == UId && u.DelFlag == DelFlag).FirstOrDefault();
            if (ResuserAction != null)
            {
                R_UserInfo_ActionInfoBLL.DeleteList(new List<int> { ResuserAction.ID });
            
            }
            return Content("ok");

        }
        #endregion
    }
}
