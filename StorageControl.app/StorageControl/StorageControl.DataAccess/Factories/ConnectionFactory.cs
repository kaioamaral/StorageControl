using StorageControl.Domain.Model.Enumerators;
using StorageControl.NetFramework.Providers;
using System.Data;
using System.Data.SqlClient;

namespace StorageControl.DataAccess.Factories
{
    public class ConnectionFactory
    {
        public IDbConnection GetConnection(ConnectionStrings connectionString)
        {
            IDbConnection con = new SqlConnection(ConfigurationProvider
                .GetConnectionString(connectionString));

            con.Open();
            return con;
        }
    }
}
