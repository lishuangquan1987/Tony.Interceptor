using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tony.Interceptor.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.TestMethod();
            test.Add(5,6);
            Console.Read();
        }
    }

   
}
