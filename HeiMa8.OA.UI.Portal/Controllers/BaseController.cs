using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.Model;
using HeiMa8.OA.Common.Cache;
using Spring.Context;
using HeiMa8.OA.IBLL;
using Spring.Context.Support;
using HeiMa8.OA.Model.Enum;

namespace HeiMa8.OA.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        //在当前的控制器里面所有的方法执行之前。都先执行此代码

        public bool IsCheckUserLogin = true;
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheckUserLogin)
            {
                //使用mm+cookie代替session
                //校验用户是否已经登录

                //从缓存中拿到当前的登录的用户信息。
                if (Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                string userGuid = Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo == null)
                {
                    //用户长时间不操作，。超时。
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                LoginUser = userInfo;
                //滑动窗口机制。
                CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20));


                //if (filterContext.HttpContext.Session["loginUser"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo; 
                //}

                #region 权限过滤

                //if (LoginUser.UName == "admin")
                //{
                //    return;//后门
                //}

                ////获取当前球的url和httpmethod
                //string url = Request.Url.AbsolutePath.ToLower();

                //string httpmethod = Request.HttpMethod.ToLower();

                ////通过容器创建一个对象 依赖注入只能在子类中实现
                //IApplicationContext ctx = ContextRegistry.GetContext();

                //IActionInfoBLL ActionInfoBLL = ctx.GetObject("ActionInfoBLL") as IActionInfoBLL;

                //IR_UserInfo_ActionInfoBLL R_UserInfo_ActionInfoBLL =
                //    ctx.GetObject("R_UserInfo_ActionInfoBLL") as IR_UserInfo_ActionInfoBLL;

                //IUserInfoBLL UserInfoBLL =
                //  ctx.GetObject("UserInfoBLL") as IUserInfoBLL;

                //short delflagNormal = (short)DelFlagEnum.Normal;
                ////获得当前请求的权限
                //List<ActionInfo> list = ActionInfoBLL.GetEntities(u => true && delflagNormal == u.DelFlag).ToList();

                //var actionInfo = ActionInfoBLL.GetEntities(u => url.StartsWith(u.Url.ToLower()) && u.HttpMethod.ToLower() == httpmethod)
                //    .FirstOrDefault();

                //var info = (from r in list
                //            where url.StartsWith(r.Url.ToLower()) && r.HttpMethod.ToLower() == httpmethod
                //            select r
                //              ).FirstOrDefault();

                //if (actionInfo == null)
                //{
                //    Response.Redirect("/Error.html");
                //}
                //是否包含当前权限 如果有 则显示数据 没有不现实
                //一号线
                //short delflagNormal = (short)DelFlagEnum.Normal;
                //拿到当前用户对应的权限
                //var rUas = R_UserInfo_ActionInfoBLL.GetEntities(u => u.UserInfoID == LoginUser.ID && u.DelFlag==delflagNormal);

                ////是否含有当前请求的权限
                //var item = (from a in rUas
                //            where a.ActionInfoID == actionInfo.ID
                //            select a).FirstOrDefault();

                //if (item != null)
                //{
                //    if (item.HasPermission == true)
                //        return;
                //    else //直接被拒绝
                //        Response.Redirect("/Error.html");
                //}

                //2号线
                //获取当前用户 因为要有导航属性
                //var user = UserInfoBLL.GetEntities(u => u.ID == LoginUser.ID).FirstOrDefault();

                ////拿到所有角色
                //var allRoles = from r in user.RoleInfo
                //               select r;
                ////通过角色拿到权限
                //var allRoleActions = from r in allRoles
                //                     from a in r.ActionInfo
                //                     select a;
                //var tmp = (from a in allRoleActions
                //           where a.ID == actionInfo.ID
                //           select a).Count();

                //if (tmp <= 0)
                //    Response.Redirect("/Error.html");

                #endregion
            }
        }

    }
}
