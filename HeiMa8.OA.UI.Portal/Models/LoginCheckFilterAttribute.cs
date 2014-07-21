using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.Model;
using HeiMa8.OA.Common.Cache;

namespace HeiMa8.OA.UI.Portal.Models
{
    public class LoginCheckFilterAttribute : ActionFilterAttribute
    {
        public bool isCheck { get; set; }

        //在所有action之前执行这个代码
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (isCheck)
            {
                //使用mm+cookie代替session
                //校验用户是否已经登录

                //从缓存中拿到当前的登录的用户信息。
                if (filterContext.HttpContext.Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                string userGuid = filterContext.HttpContext.Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo == null)
                {
                    //用户长时间不操作，。超时。
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                //设置滑动时间
                CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20));


                ////检查用户是否登陆
                //if (filterContext.HttpContext.Session["LoginUser"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
            }
        }

    }
}