using Dapper;
using StorageControl.DataAccess.Repositories.Abstractions;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using StorageControl.Domain.Model.Enumerators;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using StorageControl.DataAccess.Builders;

namespace StorageControl.DataAccess.Repositories
{
    public class CategoriesRepository : Repository, ICategoriesRepository
    {
        public int Create(Category category)
        {
            return base.Create("create_category",
                new { @name = category.Name });
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
                category.ToParameterizedObject());
        }

        public int Delete(int id)
        {
            return base.Delete("delete_category", new { @id = id });
        }
    }
}
