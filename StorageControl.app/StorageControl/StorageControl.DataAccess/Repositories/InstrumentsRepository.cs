using StorageControl.DataAccess.Repositories.Abstractions;
using StorageControl.Domain.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageControl.Domain.Model.Entities;
using StorageControl.Domain.Model.Enumerators;
using System.Data;
using Dapper;

namespace StorageControl.DataAccess.Repositories
{
    public class InstrumentsRepository : Repository, IInstrumentsRepository
    {
        public int Create(Instrument instrument)
        {
            string sql = "create_instrument";
            var parameters = new
            {
                @manufacturer = instrument.Manufacturer,
                @model = instrument.Model,
                @unit_price = instrument.UnitPrice,
                @amount = instrument.Amount,
                @category_id = instrument.Category.Id,
                @type_id = instrument.Type.Id
            };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(
                    sql: sql,
                    param: parameters,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }

        public int Delete(int id)
        {
            string sql = "delete_instrument";
            var parameter = new { @id = id };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(sql: sql, param: parameter,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public Instrument Get(int id)
        {
            string sql = "get_instrument";
            var parameter = new { @id = id };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<Instrument, Category, InstrumentType, Instrument>(
                    sql: sql,
                    map: (i, c, it) => { i.Category = c; i.Type = it; return i; },
                    param: parameter,
                    commandType: CommandType.StoredProcedure).Single();
            }
        }

        public IEnumerable<Instrument> List()
        {
            string sql = "list_instruments";

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<Instrument, Category, InstrumentType, Instrument>(
                    sql,
                    (i, c, it) => { i.Category = c; i.Type = it; return i; },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int Update(Instrument instrument)
        {
            string sql = "update_instrument";
            var parameters = new
            {
                @id = instrument.Id,
                @manufacturer = instrument.Manufacturer,
                @model = instrument.Model,
                @unit_price = instrument.UnitPrice,
                @amount = instrument.Amount,
                @category_id = instrument.Category.Id,
                @type_id = instrument.Type.Id
            };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(
                    sql: sql,
                    param: parameters,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }
    }
}
