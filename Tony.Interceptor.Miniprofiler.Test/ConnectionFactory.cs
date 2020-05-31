using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Tony.Interceptor.Miniprofiler.Test
{
   public class ConnectionFactory
    {
        private static string connStr = "Data Source=test.db";
        public static IDbConnection GetConnection()
        {
            var conn = new System.Data.SQLite.SQLiteConnection(connStr);
            return new StackExchange.Profiling.Data.ProfiledDbConnection(conn,StackExchange.Profiling.MiniProfiler.Current);
        }
    }
}
