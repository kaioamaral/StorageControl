using StorageControl.Domain.Model.Entities;
using System.Collections.Generic;

namespace StorageControl.Domain.Contracts.Interfaces
{
    public interface IInstrumentsRepository
    {
        IEnumerable<Instrument> List();

        Instrument Get(int id);

        int Create(Instrument instrument);

        int Update(Instrument instrument);

        int Delete(int id);
    }
}
