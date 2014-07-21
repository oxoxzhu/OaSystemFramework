using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;

namespace HeiMa8.OA.UI.Portal.Controllers
{
    public class WFTempController : BaseController
    {
        //
        // GET: /WFTemp/
        public IWF_TempBLL WF_TempBLL { get; set; }

        short DelFlag = (short)DelFlagEnum.Normal;
        public ActionResult Index()
        {
            return View();
        }

        #region 获取所有数据
        public ActionResult GetAllInfos()
        {
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int total = 0;
            //获取分页数据
            short DelFlag = (short)DelFlagEnum.Normal;

            //解决导航属性循环依赖的错误
            var pageData = WF_TempBLL.GetEntitiesPage(pageSize, pageIndex, out total, u => u.DelFlag == DelFlag, u => u.ID)
                .Select(u => new
                {
                    u.ID,
                    u.TempName,
                    u.ReMark,
                    u.SubTime,
                    u.ActityType,
                    u.DelFlag
                }

                );

            //匿名类
            var data = new { total = total, rows = pageData.ToList() };
            //返回json数据
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加
       
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]//非法字符不报错
        public ActionResult Add(WF_Temp WF_Temp)
        {
            WF_Temp.SubTime = DateTime.Now;
            WF_Temp.DelFlag = DelFlag;
            WF_TempBLL.Add(WF_Temp);
            return Content("ok");
        } 
        #endregion

        #region 发起流程
        public ActionResult StartWF()
        {
            ViewBag.LoginUser = LoginUser;
            ViewData.Model = WF_TempBLL.GetEntities(u => true).ToList();
            return View();
        }
        #endregion

        #region jsonp获取模板List -移动端
        public ActionResult GetTempModel()
        {

            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var Model = WF_TempBLL.GetEntities(u => true).Select(u => new { u.ID, u.TempName }).ToList();
            string json = jss.Serialize(Model);
            return JavaScript(callback + "(" + json + ")");
        }

        #endregion
    }
}
