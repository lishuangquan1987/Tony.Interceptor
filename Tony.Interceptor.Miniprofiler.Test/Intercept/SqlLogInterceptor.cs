using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using StackExchange;
using StackExchange.Profiling;

namespace Tony.Interceptor.Miniprofiler.Test.Intercept
{
    public class SqlLogInterceptor : IInterceptor
    {
        private StackExchange.Profiling.MiniProfiler miniProfiler;
        IDisposable step;
        public SqlLogInterceptor()
        {
            MiniProfiler.Settings.ProfilerProvider = new SingletonProfilerProvider();
            miniProfiler = StackExchange.Profiling.MiniProfiler.Start("test");
        }
        public void AfterInvoke(object result, MethodBase method)
        {
            step.Dispose();
            MiniProfiler.Stop();
            //Console.WriteLine($"函数：{method.Name},text:{miniProfiler.RenderPlainText()}");
            Console.WriteLine($"执行函数：{method.Name}结束");
            RecordSql(miniProfiler);
        }

        public void BeforeInvoke(MethodBase method)
        {
            step = miniProfiler.Step(method.Name);
        }
        private void RecordSql(MiniProfiler profiler)
        {
            StringBuilder sql = new StringBuilder();
            if (profiler.Root != null && profiler.Root.HasChildren)
            {
                profiler.Root.Children.ForEach(x =>
                {
                    if (x.CustomTimings != null && x.CustomTimings.Count > 0)
                    {
                        foreach (var customTimings in x.CustomTimings)
                        {
                            customTimings.Value?.ForEach(value =>
                            {
                                sql.Append(value.CommandString + "\r\n");
                            });
                        }
                    }
                });
            }
            Console.WriteLine(sql.ToString());
        }
    }
}
