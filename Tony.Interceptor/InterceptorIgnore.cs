using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tony.Interceptor
{
    /// <summary>
    /// 拦截器忽略，当一个类打了Interceptor标签后，所有的公开非静态方法默认都拦截
    /// 当打上这个标签到某个方法之后，此方法不被拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InterceptorIgnoreAttribute:Attribute
    {
    }
}
