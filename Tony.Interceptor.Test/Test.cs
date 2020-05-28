using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tony.Interceptor.Test
{
    [Interceptor(typeof(LogInterceptor))]
    public class Test:ContextBoundObject
    {
        public void TestMethod()
        {
            Console.WriteLine("执行TestMethod方法");
        }
        public int Add(int a, int b)
        {
            Console.WriteLine("执行Add方法");
            return a + b;
        }
        [InterceptorIgnore]
        public void MethodNotIntercept()
        {
            Console.WriteLine("MethodNotIntercept");
        }

    }
}
