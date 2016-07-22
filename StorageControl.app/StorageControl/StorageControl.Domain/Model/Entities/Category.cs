using StorageControl.Domain.Model.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace StorageControl.Domain.Model.Entities
{
    public class Category : Entity
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}
