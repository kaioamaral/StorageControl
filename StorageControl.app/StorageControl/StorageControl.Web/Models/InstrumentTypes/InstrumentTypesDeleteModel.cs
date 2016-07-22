using StorageControl.Domain.Model.Entities;

namespace StorageControl.Web.Models.InstrumentTypes
{
    public class InstrumentTypesDeleteModel
    {
        public InstrumentTypesDeleteModel()
        {
            this.InstrumentType = new InstrumentType();
        }

        public InstrumentType InstrumentType { get; set; }
    }
}