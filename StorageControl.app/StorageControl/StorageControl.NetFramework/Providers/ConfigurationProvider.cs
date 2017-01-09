using StorageControl.Domain.Model.Enumerators;
using System.Configuration;
using System.Linq;

namespace StorageControl.NetFramework.Providers
{
    public static class ConfigurationProvider
    {
        public static string GetConnectionString(ConnectionStrings connectionString)
        {
            return ConfigurationManager.ConnectionStrings[connectionString.ToString()].ConnectionString;
        }
    }
}
