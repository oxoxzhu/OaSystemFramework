using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using HeiMa8.OA.Common;
using Spring.Context;
using Spring.Context.Support;
using HeiMa8.OA.IBLL;
using HeiMa8.OA.Model.Enum;


namespace HeiMa8.OA.WorkFlow
{

    public sealed class SetStepCodeActivity : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> StepName { get; set; }
        public InArgument<bool> IsEnd { get; set; }
        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {


            // 获取 Text 输入参数的运行时值
            string text = context.GetValue(this.StepName);
            bool end = context.GetValue(this.IsEnd);

            Guid insId = context.WorkflowInstanceId;
            LogHelper.WriteLog("工作流实例id是：" + context.WorkflowInstanceId);
            //IApplicationContext ctx = ContextRegistry.GetContext();
            //通过容器创建一个对象 依赖注入只能在子类中实现
            IApplicationContext ctx = ContextRegistry.GetContext();

            var WF_InstanceBLL = ctx.GetObject("WF_InstanceBLL") as IWF_InstanceBLL;

            var WF_StepBLL = ctx.GetObject("WF_StepBLL") as IWF_StepBLL;
            //获取当前流程实例
            var inst = WF_InstanceBLL.GetEntities(w => w.WFInstanceId == insId).FirstOrDefault();

            if (inst == null)
            {
                Common.LogHelper.WriteLog("instance is null");
            }

            var stepStatus = (short)WFStepEnum.UnProecess;
            //获取未处理的步骤实例
            var step = inst.WF_Step.Where(s => s.StepStaus== stepStatus).FirstOrDefault();
            Common.LogHelper.WriteLog("SetStepCodeActivity:" + text);
            step.StepName = text;
            step.IsEndStep = end;
            if (end)
            {
                step.ProcessResult = "审批结束";
                inst.Status = (short)WF_InstanceEnum.Processed;
                WF_InstanceBLL.Update(inst);
            }
            WF_StepBLL.Update(step);

        }
    }
}
