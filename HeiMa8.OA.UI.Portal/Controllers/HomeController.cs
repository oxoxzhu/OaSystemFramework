using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.Common.Cache;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;

namespace HeiMa8.OA.UI.Portal.Controllers
{
    public class HomeController :BaseController
    {
        //
        // GET: /Home/
        short DelFlag = (short)DelFlagEnum.Normal;
        public IUserInfoBLL UserInfoBLL { get; set; }// = new UserInfoBLL();
        public IActionInfoBLL ActionInfoBLL { get; set; }
        public ActionResult Index()
        {
            ViewBag.AllMenu = LoadUserMenu();
            ViewBag.LoginUser = LoginUser;
            return View();
        }
        //根据用户的权限加载菜单数据
        public List<ActionInfo> LoadUserMenu()
        { 
            //拿到当前用户
            int userId = this.LoginUser.ID;
            var user = UserInfoBLL.GetEntities(u => u.ID == userId).FirstOrDefault();
            //拿到当前用户所有权限[必须是菜单]
            var allRole = user.RoleInfo;
            //第一条线  用户->角色->权限
            //吧用户对应的所有角色关联的权限的id拿出来

            var allRoleActionIds = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.ID
                                  ).ToList();
            //第二条线 特殊权限 等级比角色权限高
            //1 直接拒绝的权限
            var allDenyActionIds = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoID
                                  ).ToList();

            //角色权限-特殊拒绝权限 特殊权限 
            var allActionIds = (from r in allRoleActionIds
                                where !allDenyActionIds.Contains(r)
                                select r).ToList(); 
            //特殊直接允许权限

            var allAllowActionIds = (from r in user.R_UserInfo_ActionInfo
                                     where r.HasPermission == true
                                     select r.ActionInfoID
                                       ).ToList();

            //吧当前用户的所有权限拿到
            allActionIds.AddRange(allAllowActionIds);//第一条线+第二条线的结果


            allActionIds = allActionIds.Distinct().ToList();//去重复

            //必须是菜单 拿到所有的ActionInfo
            var FinalActionList = ActionInfoBLL.GetEntities(a => allActionIds.Contains(a.ID) && a.IsMenu == true && a.DelFlag == DelFlag).ToList();

            return FinalActionList;
        }

        public ActionResult LogOut()
        {
            string userLoginId = Request.Cookies["userLoginId"].Value;
            CacheHelper.SetCache(userLoginId, LoginUser, DateTime.Now.AddMinutes(-10000));
            //RedirectToAction("Index", "UserLogin");
            return Content("ok");
        }
    }
}
