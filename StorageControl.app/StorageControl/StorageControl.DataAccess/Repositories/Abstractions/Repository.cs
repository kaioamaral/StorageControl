using StorageControl.Domain.Model.Enumerators;
using StorageControl.NetFramework.Providers;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace StorageControl.DataAccess.Repositories.Abstractions
{
    public abstract class Repository
    {
        public IDbConnection OpenConnection(ConnectionStrings connectionString)
        {
            IDbConnection con = new SqlConnection(ConfigurationProvider.GetConnectionString(connectionString));
            con.Open();

            return con;
        }
    }
}
