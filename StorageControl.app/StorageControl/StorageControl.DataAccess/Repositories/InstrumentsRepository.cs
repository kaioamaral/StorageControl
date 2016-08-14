using Dapper;
using StorageControl.DataAccess.Builders;
using StorageControl.DataAccess.Repositories.Abstractions;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StorageControl.DataAccess.Repositories
{
    public class InstrumentsRepository : Repository, IInstrumentsRepository
    {
        public int Create(Instrument instrument)
        {
            return base.Create(sql: "create_instrument",
                param: instrument.ToParameterizedObject());
        }

        public int Delete(int id)
        {
            return base.Delete("delete_instrument", new { @id = id });
        }

        public Instrument Get(int id)
        {
            using (base.con)
                return con.Query<Instrument, Category, InstrumentType, Instrument>(
                    sql: "get_instrument",
                    map: (i, c, it) => { i.Category = c; i.Type = it; return i; },
                    param: new { @id = id },
                    commandType: CommandType.StoredProcedure).Single();
        }

        public IEnumerable<Instrument> List()
        {
            using (base.con) return con.Query<Instrument, Category, InstrumentType, Instrument>(
                    "list_instruments", (i, c, it) => { i.Category = c; i.Type = it; return i; },
                    commandType: CommandType.StoredProcedure);
        }

        public int Update(Instrument instrument)
        {
            return base.Update(sql: "update_instrument",
                param: instrument.ToParameterizedObject());
        }
    }
}
