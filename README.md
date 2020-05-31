# Welcome to Tony.Interceptor

This is a project written by C# that can intercept instance method you want

You can do something before and do something after  when you invoke the method

# why use Tony.Interceptor

You can image you have write thousands of methods.one day ,your boss requires you to add the log for each method,you are driven mad.Would you want to write the log code in each method?

or you use the third part AOP Framework?That is very heavy

No,this is the reason you use Tony.Intercetor!!!

# usage

## 1.define a class that implement the interface `IInterceptor`:

so you can handle the `BeforeInvoke` and `AfterInvoke`

```csharp
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
```

## 2.markup the class or method that you want to Intercept

First of all,the class must extend to `Interceptable`,in fact,the class `Interceptable` extends from `ContextBoundObject`,just put the class into the environment context

Then,you can use `InterceptorAttribute` to mark the class or a **instance** method in the class

If you mark the class ,it intercepts all the public instance method by default.

If you do not want to intercept a method int the marked class,you can use `InterceptorIgnoreAttribute`

```csharp
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
```

## 3.create the instance from the class and invoke the method

```csharp
class Program
{
    static void Main(string[] args)
    {
        Test test = new Test();
        test.TestMethod();
        test.Add(5,6);
        test.MethodNotIntercept();
        Console.Read();
    }
}
```

# Global Setting

this is a switch that can enable or disable the interceptor.the switch is:

```csharp
public static bool IsEnableIntercept { get; set; } = true;
```

the default value is true.if we set to false,the interceptor we have deployed is invalid

