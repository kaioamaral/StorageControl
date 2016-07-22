using StorageControl.Domain.Model.Entities;
using System.Collections.Generic;

namespace StorageControl.Domain.Contracts.Interfaces
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> List();

        Category Get(int id);

        int Create(Category category);

        int Update(Category category);

        int Delete(int id);
    }
}
