using Dapper;
using StorageControl.DataAccess.Factories;
using StorageControl.Domain.Model.Entities.Abstractions;
using StorageControl.Domain.Model.Enumerators;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StorageControl.DataAccess.Repositories.Abstractions
{
    public abstract class Repository
    {
        protected ConnectionFactory ConnectionFactory;
        protected ConnectionStrings connectionString;

        public Repository()
        {
            this.ConnectionFactory = new ConnectionFactory();
            this.connectionString = ConnectionStrings.CommerceStorage;
        }

        protected int Create(string sql, object param)
        {
            using (var con = ConnectionFactory.GetConnection(ConnectionStrings.CommerceStorage))
                return con.Query<int>(sql: sql, param: param,
                                    commandType: CommandType.StoredProcedure)
                                    .FirstOrDefault();
        }

        protected IEnumerable<T> List<T>(string sql) where T : Entity
        {
            using (var con = ConnectionFactory.GetConnection(ConnectionStrings.CommerceStorage))
                return con.Query<T>(sql: sql,
                                    commandType: CommandType.StoredProcedure);
        }

        protected T Get<T>(string sql, object param) where T : Entity
        {
            using (var con = ConnectionFactory.GetConnection(ConnectionStrings.CommerceStorage))
                return con.Query<T>(sql: sql, param: param,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault(); 
        }

        protected int Update(string sql, object param)
        {
            using (var con = ConnectionFactory.GetConnection(ConnectionStrings.CommerceStorage))
                return con.Query<int>(sql: sql, param: param,
                                    commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        protected int Delete(string sql, object param)
        {
            using (var con = ConnectionFactory.GetConnection(ConnectionStrings.CommerceStorage))
                return con.Query<int>(sql: sql, param: param,
                                    commandType: CommandType.StoredProcedure)
                                    .FirstOrDefault(); 
        }
    }
}
