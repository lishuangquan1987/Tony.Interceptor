using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace Tony.Interceptor
{
    /// <summary>
    /// 拦截器标识，此标签可以对类，对方法进行标识，标识之后会被拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class InterceptorAttribute : ContextAttribute 
    {
        public Type InterceptorHandleType;
        public InterceptorAttribute(Type iinterceptorType):base("Test")
        {
            if (iinterceptorType.GetInterface(nameof(IInterceptor))==null)
                throw new Exception("拦截器处理类必须实现IInterceptor");
            this.InterceptorHandleType = iinterceptorType;
        }
        public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
        {
            InterceptorProperty property = new InterceptorProperty();
            ctorMsg.ContextProperties.Add(property);
        }
        public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
        {
            return false;
        }
    }

}
