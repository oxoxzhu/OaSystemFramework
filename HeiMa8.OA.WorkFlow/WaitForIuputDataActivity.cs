using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace HeiMa8.OA.WorkFlow
{

    public sealed class WaitForIuputDataActivity<T> : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> InputBookMarkName { get; set; }
        public OutArgument<T> OutputData { get; set; }
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            string text = context.GetValue(this.InputBookMarkName);
            context.CreateBookmark(text , new BookmarkCallback(MethodCallback));
        }

        private void MethodCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
           // throw new NotImplementedException();
            context.SetValue(OutputData, (T)value);
        }
    }
}
