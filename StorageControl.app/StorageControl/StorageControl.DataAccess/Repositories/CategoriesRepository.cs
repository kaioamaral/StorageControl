using StorageControl.DataAccess.Builders;
using StorageControl.DataAccess.Repositories.Abstractions;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using System.Collections.Generic;

namespace StorageControl.DataAccess.Repositories
{
    public class CategoriesRepository : Repository, ICategoriesRepository
    {
        public int Create(Category category)
        {
            return base.Create("create_category",
                category.ToParameterizedObject(true));
        }

        public IEnumerable<Category> List()
        {
            return base.List<Category>("list_categories");
        }

        public Category Get(int id)
        {
            return base.Get<Category>("get_category", new { @id = id });
        }

        public int Update(Category category)
        {
            return base.Update("update_category",
                category.ToParameterizedObject(false));
        }

        public int Delete(int id)
        {
            return base.Delete("delete_category", new { @id = id });
        }
    }
}
