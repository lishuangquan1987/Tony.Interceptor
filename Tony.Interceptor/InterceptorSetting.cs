using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tony.Interceptor
{
    /// <summary>
    /// 全局的拦截器设置
    /// </summary>
    public class InterceptorSetting
    {
        /// <summary>
        /// 是否开启拦截，默认为拦截
        /// </summary>
        public static bool IsEnableIntercept { get; set; } = true;
    }
}
