using Dapper;
using StorageControl.DataAccess.Factories;
using StorageControl.Domain.Model.Entities.Abstractions;
using StorageControl.Domain.Model.Enumerators;
using StorageControl.NetFramework.Providers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StorageControl.DataAccess.Repositories.Abstractions
{
    public abstract class Repository
    {
        protected IDbConnection con;

        public Repository()
        {
            this.con = new ConnectionFactory()
                .GetConnection(ConnectionStrings.CommerceStorage);
        }
        
        public int Create(string sql, object param)
        {
            using (con) return con.Query<int>(sql: sql, param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        
        public IEnumerable<T> List<T>(string sql) where T : Entity
        {
            using (con) return con.Query<T>(sql: sql,
                    commandType: CommandType.StoredProcedure);
        }
        
        public T Get<T>(string sql, object param) where T : Entity
        {
            using (con) return con.Query<T>(sql: sql, param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault(); 
        }

        public int Update(string sql, object param)
        {
            using (con) return con.Query<int>(sql: sql, param: param,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public int Delete(string sql, object param)
        {
            using (con) return con.Query<int>(sql: sql, param: param,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault(); 
        }
    }
}
