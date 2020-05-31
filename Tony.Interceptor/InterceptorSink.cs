using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Tony.Interceptor
{
    internal class InterceptorSink : IMessageSink
    {
        /// <summary>
        /// 储存所有打了Interceptor标签的方法与其拦截器的实现类
        /// 即知道需要拦截方法的拦截器的实现类是哪一个
        /// Key:方法，Value:拦截器的类型
        /// </summary>
        private static Dictionary<MethodBase, Type> dicInterceptoredMethod = new Dictionary<MethodBase, Type>();

        IMessageSink nextSink;
        public InterceptorSink(IMessageSink nextSink)
        {
            this.nextSink = nextSink;
        }
        static InterceptorSink()
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();

            //获取程序路径下的DLL
            assemblys = assemblys.Where(x => Path.GetDirectoryName(x.Location).TrimEnd('\\')
                                 .Equals(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase)).ToArray();
            foreach (var asm in assemblys)
            {
                var types = asm.GetTypes().Where(x => x.IsClass);
                foreach (var type in types)//遍历类
                {
                    //打在类上的拦截器标签
                    var classAttribute = type.GetCustomAttributes(false)?.FirstOrDefault(x => x is InterceptorAttribute) as InterceptorAttribute;
                    //遍历方法
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))//遍历类中的方法
                    {
                        var methodAttribute = method.GetCustomAttributes(false)?.FirstOrDefault(x => x is InterceptorAttribute) as InterceptorAttribute;
                        var methodUnInterceptorAttribute = method.GetCustomAttributes(false)?.FirstOrDefault(x => x is InterceptorIgnoreAttribute);
                        if (methodUnInterceptorAttribute != null)//打了忽略拦截器标签后，不拦截
                        {
                            continue;
                        }

                        if (methodAttribute != null)//在方法上打标签优先级最高
                        {
                            dicInterceptoredMethod.Add(method, methodAttribute.InterceptorHandleType);
                        }
                        else if (classAttribute != null)
                        {
                            dicInterceptoredMethod.Add(method, classAttribute.InterceptorHandleType);
                        }
                    }
                }
            }

        }
        public IMessageSink NextSink => nextSink;

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return null;
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            var message = msg as IMethodMessage;
            if (InterceptorSetting.IsEnableIntercept && message != null)
            {
                if (dicInterceptoredMethod.ContainsKey(message.MethodBase))
                {
                    var intercepterType = dicInterceptoredMethod[message.MethodBase];
                    var interceptor = Activator.CreateInstance(intercepterType) as IInterceptor;
                    interceptor.BeforeInvoke(message.MethodBase);
                    var resultMessage = nextSink.SyncProcessMessage(message);
                    interceptor.AfterInvoke(resultMessage.Properties["__Return"], message.MethodBase);
                    return resultMessage;
                }
                else
                {
                    return nextSink.SyncProcessMessage(message);
                }
            }
            else
            {
                return nextSink.SyncProcessMessage(msg);
            }
        }
    }
}
