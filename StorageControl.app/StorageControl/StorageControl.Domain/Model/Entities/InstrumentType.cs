using StorageControl.Domain.Model.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace StorageControl.Domain.Model.Entities
{
    public class InstrumentType : Entity
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}
