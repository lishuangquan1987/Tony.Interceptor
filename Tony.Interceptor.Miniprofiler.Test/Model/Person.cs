using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tony.Interceptor.Miniprofiler.Test.Model
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedTime { get; set; }
        public override string ToString()
        {
            return $"ID:{ID},Name:{Name},Age:{Age},CreatedTime:{CreatedTime}";
        }
    }
}
