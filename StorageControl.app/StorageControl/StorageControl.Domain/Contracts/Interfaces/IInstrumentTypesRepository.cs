using StorageControl.Domain.Model.Entities;
using System.Collections.Generic;

namespace StorageControl.Domain.Contracts.Interfaces
{
    public interface IInstrumentTypesRepository
    {
        IEnumerable<InstrumentType> List();

        InstrumentType Get(int id);

        int Create(InstrumentType instrumentType);

        int Update(InstrumentType instrumentType);

        int Delete(int id);
    }
}
