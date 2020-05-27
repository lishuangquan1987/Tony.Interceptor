using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Tony.Interceptor
{
    public class InterceptorProperty : IContextProperty, IContributeServerContextSink
    {
        public string Name => nameof(InterceptorProperty);

        public void Freeze(Context newContext)
        {
            
        }
        /// <summary>
        /// 系统会自动调用该方法。通过调用该方法系统会将我们自定义的Sink插入到对象调用的消息传递链中，这样就可以
        //  在自定义的Sink中拦截方法调用
        /// </summary>
        /// <param name="nextSink"></param>
        /// <returns></returns>
        public IMessageSink GetServerContextSink(IMessageSink nextSink)
        {
            InterceptorSink sink = new InterceptorSink(nextSink);
            return sink;
        }
        /// <summary>
        /// 判断一个context是否已经有InterceptorProperty，如果有就用这个context，如果没有则新建一个
        /// </summary>
        /// <param name="newCtx"></param>
        /// <returns></returns>
        public bool IsNewContextOK(Context newCtx)
        {
            IContextProperty property = newCtx.GetProperty(this.Name);
            if (property == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
