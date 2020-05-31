using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Tony.Interceptor.Miniprofiler.Test.Intercept;
using Tony.Interceptor.Miniprofiler.Test.Model;

namespace Tony.Interceptor.Miniprofiler.Test.Dao
{
    [Interceptor(typeof(SqlLogInterceptor))]
    public class PersonDao:Interceptable
    {
        public List<Person> GetPersons()
        {
            string sql = "select * from person";
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query<Person>(sql).ToList();
            }
        }
        public List<Person> GetPersonById(int id)
        {
            string sql = "select * from person where id=@id";
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Query<Person>(sql,new { id}).ToList();
            }
        }
        public void AddPerson(Person p)
        {
            string sql = "insert into person(name,age,createdtime) values(@name,@age,@createdtime)";
            using (var conn = ConnectionFactory.GetConnection())
            {
                conn.Execute(sql,p);
            }
        }
        public void AddPerson(List<Person> persons)
        {
            string sql = "insert into person(name,age,createdtime) values(@name,@age,@createdtime)";
            using (var conn = ConnectionFactory.GetConnection())
            {
                conn.Execute(sql, persons);
            }
        }
    }
}
