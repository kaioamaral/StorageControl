using StorageControl.Domain.Model.Entities;

namespace StorageControl.Web.Models.Instruments
{
    public class InstrumentsDetailsModel
    {
        public InstrumentsDetailsModel()
        {
            this.Instrument = new Instrument();
        }

        public Instrument Instrument { get; set; }
    }
}