using StorageControl.Domain.Model.Entities;

namespace StorageControl.Web.Models.Instruments
{
    public class InstrumentsDeleteModel
    {
        public InstrumentsDeleteModel()
        {
            this.Instrument = new Instrument();
        }

        public Instrument Instrument { get; set; }
    }
}