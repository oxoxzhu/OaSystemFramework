using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.BLL;
using HeiMa8.OA.Common;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;
using HeiMa8.OA.Common.Cache;
using HeiMa8.OA.UI.Portal.Models;

namespace HeiMa8.OA.UI.Portal.Controllers
{
    [LoginCheckFilter(isCheck = false)]
    public class UserLoginController : Controller
    {
        //
        // GET: /UserLogin/

        public IUserInfoBLL UserInfoBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 验证码
        public ActionResult ShowVCode()
        {
            ValidateCode validateCode = new ValidateCode();

            string strCode = validateCode.CreateValidateCode(4);

            //
            Session["VCode"] = strCode;


            byte[] imgByt = validateCode.CreateValidateGraphic(strCode);

            return File(imgByt, @"image/jpeg");

        }
        #endregion

        #region 处理登陆的表单
        public ActionResult ProcessLogin()
        {
            //获取表单提交过来的数据

            //获取表单中的验证码 
            string vCode = Request["vCode"];

            string SessionCode = Session["VCode"] as string;

            Session["VCode"] = null;//去一次用一次

            if (string.IsNullOrEmpty(SessionCode) || SessionCode != vCode)
            {
                return Content("验证码错误");
            }

            //验证登陆
            string LoginName = Request["LoginCode"];

            string LoginPwd = Request["LoginPwd"];

            short DelFlag = (short)DelFlagEnum.Normal;

            UserInfo userInfo = UserInfoBLL.GetEntities(u => u.UName == LoginName && u.Pwd == LoginPwd && u.DelFlag == DelFlag).FirstOrDefault();

            if (userInfo == null)
            {
                return Content("用户名或密码错误");
            }
            //Session["LoginUser"] = userInfo;

            ////Session["loginUser"] = userInfo;
            //用memcache+cookie代替之。

            //立即分配一个标志，Guid。把标志作为 mm存储数据的key，把用户对象放到 mm。 把guid写到客户端cookie里面去。
            string userLoginId = Guid.NewGuid().ToString();
            //把用户的数据写到mm,怎么解决变化：写到不同地方去，可能同时写入多个地方。
            CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));

            //往客户端写入cookie
            Response.Cookies["userLoginId"].Value = userLoginId;

            return Content("ok");
        }
        #endregion
    }
}
