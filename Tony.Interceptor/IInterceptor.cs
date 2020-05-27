using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tony.Interceptor
{
    public interface IInterceptor
    {
        /// <summary>
        /// 执行方法之前处理
        /// </summary>
        /// <param name="method"></param>
        void BeforeInvoke(MethodBase method);
        /// <summary>
        /// 执行方法之后处理
        /// </summary>
        /// <param name="result"></param>
        /// <param name="method"></param>
        void AfterInvoke(object result, MethodBase method);
    }
}
