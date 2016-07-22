using StorageControl.Domain.Model.Entities;

namespace StorageControl.Web.Models.Categories
{
    public class CategoriesDeleteModel
    {
        public CategoriesDeleteModel()
        {
            this.Category = new Category();
        }

        public Category Category { get; set; }
    }
}