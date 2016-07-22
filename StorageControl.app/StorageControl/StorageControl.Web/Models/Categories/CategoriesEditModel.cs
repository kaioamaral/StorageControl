using StorageControl.Domain.Model.Entities;

namespace StorageControl.Web.Models.Categories
{
    public class CategoriesEditModel
    {
        public CategoriesEditModel()
        {
            this.Category = new Category();
        }

        public Category Category { get; set; }
    }
}