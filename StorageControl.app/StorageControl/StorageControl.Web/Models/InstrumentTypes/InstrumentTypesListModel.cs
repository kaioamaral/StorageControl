using StorageControl.Domain.Model.Entities;
using System.Collections.Generic;

namespace StorageControl.Web.Models.InstrumentTypes
{
    public class InstrumentTypesListModel
    {
        public InstrumentTypesListModel()
        {
            this.InstrumentTypes = new List<InstrumentType>();
        }

        public List<InstrumentType> InstrumentTypes { get; set; }
    }
}