using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tony.Interceptor.Test
{
    class LogInterceptor : IInterceptor
    {
        public void AfterInvoke(object result, MethodBase method)
        {
            Console.WriteLine($"执行{method.Name}完毕，返回值：{result}");
        }

        public void BeforeInvoke(MethodBase method)
        {
            Console.WriteLine($"准备执行{method.Name}方法");
        }
    }
}
