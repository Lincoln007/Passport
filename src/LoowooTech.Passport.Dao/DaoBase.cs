using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Dao
{
    public class DaoBase
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public System.Data.IDbConnection GetConnection()
        {
            return new Oracle.ManagedDataAccess.Client.OracleConnection(connectionString);
        }
    }
}
