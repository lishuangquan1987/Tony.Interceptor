using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tony.Interceptor
{
    /// <summary>
    /// 拦截器拦截处理接口
    /// </summary>
    public interface IInterceptHandler
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
