using Dapper;
using StorageControl.DataAccess.Repositories.Abstractions;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using StorageControl.Domain.Model.Enumerators;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StorageControl.DataAccess.Repositories
{
    public class InstrumentTypesRepository : Repository, IInstrumentTypesRepository
    {
        public int Create(InstrumentType instrumentType)
        {
            string sql = "create_instrument_type";
            var parameters = new { @name = instrumentType.Name };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(sql: sql, param: parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int Delete(int id)
        {
            string sql = "delete_instrument_type";
            var parameters = new { @id = id };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(sql: sql, param: parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public InstrumentType Get(int id)
        {
            string sql = "get_instrument_type";
            var parameters = new { @id = id };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<InstrumentType>(sql: sql, param: parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<InstrumentType> List()
        {
            string sql = "list_instrument_types";

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<InstrumentType>(sql: sql,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int Update(InstrumentType instrumentType)
        {
            string sql = "update_instrument_type";
            var parameters = new { @id = instrumentType.Id, @name = instrumentType.Name };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(sql: sql, param: parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
