using StorageControl.Domain.Model.Entities;
using System.Collections.Generic;

namespace StorageControl.Web.Models.Instruments
{
    public class InstrumentsListModel
	{
        public InstrumentsListModel()
        {
            this.Instruments = new List<Instrument>();
        }

        public List<Instrument> Instruments { get; set; }
    }
}