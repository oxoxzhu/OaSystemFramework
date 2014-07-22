using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HeiMa8.OA.Common;

namespace HeiMa8.OA.WorkFlow
{
    public class WorkflowApplicationHelper
    {
        private static readonly string StrSql;
        static WorkflowApplicationHelper()
        {
            StrSql = ConfigurationManager.ConnectionStrings["wfsql"].ConnectionString;
        }

        //创建工作流 并持久化到数据库中
        public static WorkflowApplication CreateWorkflowApp(Activity activity,Dictionary<string,object> dictParm)
        {

            AutoResetEvent atuoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp;

            if (dictParm == null)
            {
                wfApp = new WorkflowApplication(activity);//根据指定的工作流 创建WorkflowAppdu对象

            }
            else
            {
                wfApp = new WorkflowApplication(activity, dictParm);
            }

            wfApp.Idle += a =>//当工作流停下来的时候，执行此事件响应方法
            {
                Console.WriteLine("工作流停下来了。。");
                LogHelper.WriteLog("工作流停下来了");
            };

            //当咱们工作流停顿下来的时候，进行什么操作。如果返回是Unload。那么就卸载当前
            //工作流实例，持久化到数据库里面去。
            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e2)
            {
                Console.WriteLine("工作卸载进行持久化,书签被创建时候就会执行序列化到数据库里面去。");
                Common.LogHelper.WriteLog("工作卸载进行持久化");
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a =>
            {
                atuoResetEvent.Set();
                Console.WriteLine("工作流被卸载了");
                LogHelper.WriteLog("工作流被卸载了");
            };
            
            wfApp.OnUnhandledException += a =>//工作流异常的时候 响应的方法
            {
                atuoResetEvent.Set();
                Console.WriteLine("出现了异常");

                LogHelper.WriteLog(a.UnhandledException.ToString());//记录异常信息
                return UnhandledExceptionAction.Terminate;//直接结束工作流

            };

            wfApp.Aborted += a =>
            {
                atuoResetEvent.Set();
                Console.WriteLine("Aborted");
            };
            //工作流持久化
            //创建一个保存 工作流实例的sqlstore对象。
            SqlWorkflowInstanceStore store =new SqlWorkflowInstanceStore(StrSql);

            //在wfApp持久化的时候，保存到上面对象所配置的数据库里面去
            wfApp.InstanceStore = store;
            wfApp.Run();
            return wfApp;
        }


        //恢复书签 拿到在数据库中的实例 继续执行
        public static WorkflowApplication ResumeBookMark(Activity activity,Guid instanceid,string bookMarkName,object value)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp = new WorkflowApplication(activity);
            wfApp.Idle += a =>//当工作流停下来的时候，执行此事件响应方法。
            {
                Console.WriteLine("工作流停下来了...");
            };

            //工作流停下的时候，进行的操作

            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e3)
            {
                Console.WriteLine("工作卸载进行持久化");

                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a =>
            {

                LogHelper.WriteLog("工作流被卸载了。。");
                autoResetEvent.Set();

                Console.WriteLine("工作流被卸载了");
            };

            wfApp.OnUnhandledException += a =>//工作流异常的时候 响应的方法
            {
                autoResetEvent.Set();
                Console.WriteLine("出现了异常");

                LogHelper.WriteLog(a.UnhandledException.ToString());//记录异常信息
                return UnhandledExceptionAction.Terminate;//直接结束工作流

            };

            wfApp.Aborted += a =>//终止的时候 响应的方法
            {
                autoResetEvent.Set();
                Console.WriteLine("Aborted");
            };

            //创建一个保存 工作流实例的sqlstore对象。
            SqlWorkflowInstanceStore store =new SqlWorkflowInstanceStore(StrSql);

            //wfApp在进行持久化的时候，保存到上面对象配置的数据库里面去。
            wfApp.InstanceStore = store;

            //从数据库中获得当前工作流实例的状态
            wfApp.Load(instanceid);

            //工作流继续执行
            wfApp.ResumeBookmark(bookMarkName, value);

            return wfApp;
        }
    }
}
