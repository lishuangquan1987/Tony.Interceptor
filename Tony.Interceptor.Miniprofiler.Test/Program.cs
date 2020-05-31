using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tony.Interceptor.Miniprofiler.Test.Dao;
using Tony.Interceptor.Miniprofiler.Test.Model;

namespace Tony.Interceptor.Miniprofiler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonDao personDao = new PersonDao();
            List<Person> persons = new List<Person>()
            {
                new Person(){ Name="Tony1",Age=18,CreatedTime=DateTime.Now},
                new Person(){ Name="Tony2",Age=19,CreatedTime=DateTime.Now.AddDays(1)},
                new Person(){ Name="Tony3",Age=20,CreatedTime=DateTime.Now.AddDays(2)},
                new Person(){ Name="Tony4",Age=21,CreatedTime=DateTime.Now.AddDays(3)},
                new Person(){ Name="Tony5",Age=22,CreatedTime=DateTime.Now.AddDays(4)},
                new Person(){ Name="Tony6",Age=23,CreatedTime=DateTime.Now.AddDays(5)},
            };
            personDao.AddPerson(persons);

            var list= personDao.GetPersons();
            //Console.WriteLine(list);
            Console.ReadLine();

        }
    }
}
