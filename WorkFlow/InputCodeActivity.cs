using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WorkFlow
{

    public sealed class InputCodeActivity : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public OutArgument<string> InputMoney { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            string text = context.GetValue(this.InputMoney);

            //创建书签 需要继承NativeActivity


        }
    }
}
