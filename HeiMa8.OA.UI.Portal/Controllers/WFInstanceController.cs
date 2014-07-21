using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HeiMa8.OA.Common.Cache;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model;
using HeiMa8.OA.Model.Enum;
using HeiMa8.OA.WorkFlow;
using Spring.Context;
using Spring.Context.Support;

namespace HeiMa8.OA.UI.Portal.Controllers
{
    public class WFInstanceController : BaseController
    {
        //
        // GET: /WFInstance/
        public IWF_TempBLL WF_TempBLL { get; set; }
        public IUserInfoBLL UserInfoBLL { get; set; }
        public IWF_InstanceBLL WF_InstanceBLL { get; set; }
        public IWF_StepBLL WF_StepBLL { get; set; }
        short DelFlag = (short)DelFlagEnum.Normal;
        public ActionResult Index()
        {
            return View();
        }

        //发起流程 
        public ActionResult Add(int id)
        {
            //记录当前ID
            //WF_Add_ID = id;
            Session["WF_Add_ID"] = id.ToString();
            ViewBag.Temp = WF_TempBLL.GetEntities(u => u.ID == id).FirstOrDefault();
            //获取所有用户
            var allUsers = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();

            ViewData["flowTo"] = (from r in allUsers
                                  select new SelectListItem { Selected = false, Text = r.UName, Value = r.ID + "" }
                                  ).ToList();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WF_Instance instance, int id, int flowTo)
        {

            var CurrentUserId = LoginUser.ID;

            //1->在工作流实例表中添加一条数据
            instance.DelFlag = DelFlag;
            instance.StartTime = DateTime.Now;
            instance.FilePath = string.Empty;

            instance.StartBy = CurrentUserId;//当前用户 也就是发起人
            instance.Level = 0;//紧急程度
            instance.Status = (short)WF_InstanceEnum.Processing;
            instance.WF_TempID = id;

            //WF_TempBLL.Add(instance);
            WF_InstanceBLL.Add(instance);


            //2->在步骤表里面添加两条步骤
            //2->1 一个当前已处理的步骤
            WF_Step startStep = new WF_Step();
            startStep.WF_InstanceID = instance.ID;
            startStep.SubTime = DateTime.Now;
            startStep.StepName = "提交申请表单";
            startStep.IsEndStep = false;
            startStep.IsStartStep = true;
            startStep.ProcessBy = CurrentUserId;
            startStep.ProcessContent = "提交申请";
            startStep.ProcessResult = "通过";
            startStep.ProcessTime = DateTime.Now;
            startStep.StepStaus = (short)WFStepEnum.Processed;
            WF_StepBLL.Add(startStep);

            //2->2 下一步谁审批的步骤 。项目经理审批
            WF_Step nextStep = new WF_Step();
            nextStep.WF_InstanceID = instance.ID;
            nextStep.SubTime = DateTime.Now;
            nextStep.ProcessTime = DateTime.Now;
            nextStep.ProcessContent = string.Empty;

            nextStep.IsStartStep = false;
            nextStep.IsEndStep = false;
            nextStep.ProcessBy = flowTo;

            nextStep.ProcessResult = "";
            nextStep.StepName = "";
            nextStep.StepStaus = (short)WFStepEnum.UnProecess;
            WF_StepBLL.Add(nextStep);

            //3->启动工作流
            //根据流程类型 来初始化对应流程 spring 实现
            //通过容器创建一个对象 依赖注入只能在子类中实现
            // IApplicationContext ctx = ContextRegistry.GetContext();
            var wftemp = WF_TempBLL.GetEntities(u => u.ID == id).FirstOrDefault();
            //Activity activityType = ctx.GetObject(wftemp.ActityType.Split('.')[3]) as Activity;

            var wfApp = WorkflowApplicationHelper.CreateWorkflowApp(
                //new FincallActivity(),
                Assembly.Load("HeiMa8.OA.WorkFlow").CreateInstance(wftemp.ActityType) as Activity,
                null);
            instance.WFInstanceId = wfApp.Id;
            WF_InstanceBLL.Update(instance);


            return Content("ok");
        }

        #region 我的审批流程
        //自己发起的流程
        public ActionResult ShowMyCheck()
        {

            var data = WF_InstanceBLL.GetEntities(i => i.StartBy == LoginUser.ID).ToList();
            //获得所有用户
            ViewBag.AllUsers = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();
            return View(data);
        }

        //显示已经审批(还没结束的流程)
        public ActionResult ShowMyChecked()
        {

            var runEnum = (short)WFStepEnum.Processed;

            var Steps = WF_StepBLL.GetEntities(s => s.StepStaus == runEnum && s.ProcessBy == LoginUser.ID).ToList();

            //当前已经审批的所有Id
            var instanceIds = (from s in Steps
                               select s.WF_InstanceID).Distinct();
            var data = WF_InstanceBLL.GetEntities(u => instanceIds.Contains(u.ID)).ToList();

            //获得所有用户
            ViewBag.AllUsers = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();
            return View(data);
        }

        //显示没审批
        public ActionResult ShowMyUnCheck()
        {

            var runEnum = (short)WFStepEnum.UnProecess;
            var instanceEnum = (short)WF_InstanceEnum.Processing;

            var Steps = WF_StepBLL.GetEntities(s => s.StepStaus == runEnum && s.ProcessBy == LoginUser.ID).ToList();

            //当前待审批的所有Id
            var instanceIds = (from s in Steps
                               select s.WF_InstanceID).Distinct();
            var data = WF_InstanceBLL.GetEntities(u => instanceIds.Contains(u.ID) && u.Status == instanceEnum).ToList();

            //获得所有用户
            ViewBag.AllUsers = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();
            return View(data);
        }
        #endregion


        #region 详情
        public ActionResult ShowInstance(int id)
        {
            Session["InstanceID"] = id.ToString();
            ViewData.Model = WF_InstanceBLL.GetEntities(u => u.ID == id).FirstOrDefault();
            //获得所有用户
            ViewBag.AllUsers = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();
            return View();
        }
        #endregion


        #region 审批

        //审批的情况
        public ActionResult ShowCheck(int id)
        {
            Session["SelectedInstanceID"] = id.ToString();
            //当前流程的详情
            var data = WF_InstanceBLL.GetEntities(u => u.ID == id).FirstOrDefault();
            ViewData.Model = data;
            var currentStep = data.WF_Step.Where(s => s.StepStaus == 0).FirstOrDefault();
            //获得所有用户
            ViewBag.AllUsers = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList();
            ViewData["flowTo"] = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).ToList()
                .Select(u => new SelectListItem { Selected = false, Text = u.UName, Value = u.ID + "" });
            return View();
        }

        //审批当前流程
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="stepId">当前审批步骤id</param>
        /// <param name="isPass">是否通过</param>
        /// <param name="Comment">审批意见</param>
        /// <param name="flowTo">流转到谁</param>
        /// <returns></returns>
        public ActionResult DoCheck(int stepId, bool isPass, string Comment, int flowTo)
        {

            //1.更新当前步骤

            var step = WF_StepBLL.GetEntities(u => u.ID == stepId).FirstOrDefault();

            step.ProcessResult = isPass ? "通过" : "不通过";
            step.StepStaus = (short)WFStepEnum.Processed;
            step.ProcessContent = Comment;

            step.ProcessTime = DateTime.Now;
            WF_StepBLL.Update(step);
            //初始化下一个步骤
            WF_Step nextStep = new WF_Step();
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = flowTo;

            nextStep.SubTime = DateTime.Now;
            nextStep.ProcessResult = string.Empty;

            nextStep.StepStaus = (short)WFStepEnum.UnProecess;
            nextStep.StepName = string.Empty;
            nextStep.WF_InstanceID = step.WF_InstanceID;
            nextStep.ProcessTime = DateTime.Now;

            nextStep.ProcessContent = string.Empty;
            WF_StepBLL.Add(nextStep);


            //根据流程类型 来初始化对应流程 反射 实现

            var wfinstance = WF_InstanceBLL.GetEntities(u => u.ID == step.WF_InstanceID).FirstOrDefault();
            var wftemp = WF_TempBLL.GetEntities(u => u.ID == wfinstance.WF_TempID).FirstOrDefault();
            //让书签继续往下执行
            var Value = isPass ? 1 : 0;
            WorkflowApplicationHelper.ResumeBookMark(
                //new FincallActivity(),
                Assembly.Load("HeiMa8.OA.WorkFlow").CreateInstance(wftemp.ActityType) as Activity,
                step.WF_Instance.WFInstanceId,
                step.StepName,
                Value);
            return Content("ok");
        }

        #endregion




        #region 获取当前用户 jsonp返回 -移动端获取

        public ActionResult GetCurrentUser()
        {
            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
           // string userGuid = Request.Cookies["userLoginId"].Value;
            //UserInfo userInfo = CacheHelper.GetCache(userGuid) as UserInfo;
            string json = jss.Serialize(LoginUser.UName);
            return JavaScript(callback + "(" + json + ")");
        }

        #endregion

        #region 获取当前流程模板 jsonp返回 -移动端获取

        public ActionResult GetTempModelByID()
        {

            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int id = int.Parse(Session["WF_Add_ID"] as string);
            var data = WF_TempBLL.GetEntities(u => u.ID == id).Select(u => new { u.TempName, u.TempForm, u.Description, u.ID }).FirstOrDefault();
            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");
        }

        #endregion

        #region 记录一下ShowCheck的ID
        public ActionResult SaveCheckID(int id)
        {
            Session["SelectedInstanceID"] = id.ToString();
            return Content("ok");
        }
        #endregion

        #region 记录一下ShowIntance的ID
        public ActionResult SaveInstanceID(int id)
        {
            Session["InstanceID"] = id.ToString();
            return Content("ok");
        }
        #endregion

        #region 记录一下WFInstanceAdd的ID
        public ActionResult SaveAddInstanceID(int id)
        {
            Session["WF_Add_ID"] = id.ToString();
            return Content("ok");
        }

        #endregion

        #region 获取所有用户 jsonp返回  移动端
        public ActionResult GetAllUses()
        {
            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //获取所有用户
            var data = UserInfoBLL.GetEntities(u => u.DelFlag == DelFlag).Select(u => new
            {
                u.UName,
                u.ID
            }).ToList();
            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");
        }

        #endregion

        #region 获取当前用户发起的流程 jsonp返回 移动端
        public ActionResult GetMyCheck()
        {
            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //获取当前用户发起的流程
            var data = WF_InstanceBLL.GetEntities(i => i.StartBy == LoginUser.ID).Select(u => new
            {
                u.ID,
                u.InstanceName,
                u.WF_TempID,
                u.StartTime,
                u.StartBy,
                u.Status

            }).ToList();
            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");

        }
        #endregion
        
        #region 获取当前用户已经审批的流程 jsonp返回 移动端
        public ActionResult GetMyChecked()
        {
            var runEnum = (short)WFStepEnum.Processed;
            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var Steps = WF_StepBLL.GetEntities(s => s.StepStaus == runEnum && s.ProcessBy == LoginUser.ID).ToList();

            //当前已经审批的所有Id
            var instanceIds = (from s in Steps
                               select s.WF_InstanceID).Distinct();
            //获取已经审批流程的数据
            var data = WF_InstanceBLL.GetEntities(u => instanceIds.Contains(u.ID)).Select(u => new
            {
                u.ID,
                u.InstanceName,
                u.WF_TempID,
                u.StartTime,
                u.StartBy,
                u.Status

            }).ToList();
            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");
        }
        #endregion

        #region 获取当前用户待审批的流程 jsonp返回 移动端
        public ActionResult GetMyUnCheck()
        {
            var runEnum = (short)WFStepEnum.UnProecess;
            var instanceEnum = (short)WF_InstanceEnum.Processing;

            var Steps = WF_StepBLL.GetEntities(s => s.StepStaus == runEnum && s.ProcessBy == LoginUser.ID).ToList();

            //当前待审批的所有Id
            var instanceIds = (from s in Steps
                               select s.WF_InstanceID).Distinct();
            //获取当前待审批流程的数据
            var data = WF_InstanceBLL.GetEntities(u => instanceIds.Contains(u.ID) && u.Status == instanceEnum).Select(u => new
            {
                u.ID,
                u.InstanceName,
                u.WF_TempID,
                u.StartTime,
                u.StartBy,
                u.Status

            }).ToList();


            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");

        }
        #endregion

        #region 根据ID获取当前实例 jsonp返回 移动端
        public ActionResult GetInstanceByID()
        {
            string ID = Request["ID"] as string;
            int t = int.Parse(Request["type"]);
            int id;
            if (t == 0)
                id = int.Parse(Session["InstanceID"] as string);
            else
                id = int.Parse(Session["SelectedInstanceID"] as string);
            //当前流程实例
            var data = WF_InstanceBLL.GetEntities(u => u.ID == id).Select(u => new
            {
                u.ID,
                u.InstanceName,
                u.WF_TempID,
                u.StartTime,
                u.StartBy,
                u.Content,
            }
                ).FirstOrDefault();

            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");
        }
        #endregion

        #region 根据ID获取当前实例步骤 jsonp返回 移动端
        public ActionResult GetInstanceStepByID()
        {
            int t = int.Parse(Request["type"]);
            int id;//实例ID
            if (t == 0)
                id = int.Parse(Session["InstanceID"] as string);
            else
                id = int.Parse(Session["SelectedInstanceID"] as string);
            //当前流程实例
            var data = WF_StepBLL.GetEntities(u => u.WF_InstanceID == id).Select(u => new
            {
                u.StepName,
                u.ProcessBy,
                u.SubTime,
                u.ProcessContent,
                u.ProcessResult
            }).ToList();
            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");
        }
        #endregion

        #region 获取当前实例的stepstus==0的stepid
        public ActionResult GetCurrentInstanceStepID()
        {
            int id = int.Parse(Session["SelectedInstanceID"] as string);
            var data = WF_InstanceBLL.GetEntities(u => u.ID == id).FirstOrDefault();
            var currentStep = data.WF_Step.Where(s => s.StepStaus == 0).FirstOrDefault();

            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string json = jss.Serialize(currentStep.ID.ToString());
            return JavaScript(callback + "(" + json + ")");
        }
        #endregion

        #region 获取当前用户的已审批，待审批，总的流程分别的个数

        public ActionResult GetAllTypeCount()
        {
            #region 获取当前用户发起的流程个数 MyCheckCount

            var MyCheckCount = WF_InstanceBLL.GetEntities(i => i.StartBy == LoginUser.ID).ToList().Count;

            #endregion

            #region 获取当前用户已审批的流程个数 MyCheckedCount

            var runEnum = (short)WFStepEnum.Processed;

            var Steps = WF_StepBLL.GetEntities(s => s.StepStaus == runEnum && s.ProcessBy == LoginUser.ID).ToList();

            //当前已经审批的所有Id
            var instanceIds = (from s in Steps
                               select s.WF_InstanceID).Distinct();
            var MyCheckedCount = WF_InstanceBLL.GetEntities(u => instanceIds.Contains(u.ID)).ToList().Count;

            #endregion

            #region 获取当前用户待审批的流程个数 MyUnCheckCount

            var runEnumed = (short)WFStepEnum.UnProecess;
            var instanceEnum = (short)WF_InstanceEnum.Processing;

            var Stepsed = WF_StepBLL.GetEntities(s => s.StepStaus == runEnumed && s.ProcessBy == LoginUser.ID).ToList();


            //当前待审批的所有Id
            var instanceIdsed = (from s in Stepsed
                               select s.WF_InstanceID).Distinct();
            var MyUnCheckCount = WF_InstanceBLL.GetEntities(u => instanceIdsed.Contains(u.ID) && u.Status == instanceEnum).ToList().Count;
            #endregion

            string callback = Request["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var data = new { CheckCount = MyCheckCount, CheckedCount = MyCheckedCount, UnCheckCount = MyUnCheckCount };
            string json = jss.Serialize(data);
            return JavaScript(callback + "(" + json + ")");
        }
        #endregion
    }
}
