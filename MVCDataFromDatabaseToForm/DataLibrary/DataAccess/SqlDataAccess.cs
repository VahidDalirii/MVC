using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {

        public static string GetConnectionString(string connectionName="MvcEmployeeDb")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }


        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn=new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }


        public static void SaveData<T>(string sql, T data)
        {
            using(IDbConnection cnn=new SqlConnection(GetConnectionString()))
            {
                cnn.Execute(sql, data);
            }
        }
    }
}
