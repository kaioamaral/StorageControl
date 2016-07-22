using StorageControl.Domain.Model.Entities;

namespace StorageControl.Web.Models.Categories
{
    public class CategoriesCreateModel
    {
        public Category Category { get; set; }

        public CategoriesCreateModel()
        {
            this.Category = new Category();
        }
    }
}