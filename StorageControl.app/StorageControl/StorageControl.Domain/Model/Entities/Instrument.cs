using StorageControl.Domain.Model.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace StorageControl.Domain.Model.Entities
{
    public class Instrument : Entity
    {
        [Display(Name = "Fabricante")]
        public string Manufacturer { get; set; }
        
        [Display(Name = "Modelo")]
        public string Model { get; set; }
        
        [Display(Name = "Preço Unitário")]
        public decimal UnitPrice { get; set; }
        
        [Display(Name = "Quantidade")]
        public int Amount { get; set; }
        
        [Display(Name = "Categoria")]
        public Category Category { get; set; }

        [Display(Name = "Tipo")]
        public InstrumentType Type { get; set; }
    }
}
