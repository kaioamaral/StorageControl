using StorageControl.Domain.Model.Entities;

namespace StorageControl.Web.Models.InstrumentTypes
{
    public class InstrumentTypesEditModel
    {
        public InstrumentTypesEditModel()
        {
            this.InstrumentType = new InstrumentType();
        }

        public InstrumentType InstrumentType { get; set; }
    }
}