using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.Common;

namespace HeiMa8.OA.UI.Portal.Models
{
    public class MyExceptionFilterAttribute : HandleErrorAttribute
    {
        // //只要发现异常 就会执行这个事件
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

           
            //直接把错误信息写道日志文件中

            Common.LogHelper.WriteLog(filterContext.Exception.ToString());

        }
    }
}