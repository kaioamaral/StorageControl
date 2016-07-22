using Dapper;
using StorageControl.DataAccess.Repositories.Abstractions;
using StorageControl.Domain.Contracts.Interfaces;
using StorageControl.Domain.Model.Entities;
using StorageControl.Domain.Model.Enumerators;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StorageControl.DataAccess.Repositories
{
    public class CategoriesRepository : Repository, ICategoriesRepository
    {
        public int Create(Category category)
        {
            string sql = "create_category";
            var parameter = new { @name = category.Name };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(sql: sql, param: parameter,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int Delete(int id)
        {
            string sql = "delete_category";
            var parameter = new { @id = @id };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(sql: sql, param: parameter,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }

        public Category Get(int id)
        {
            string sql = "get_category";
            var parameter = new { @id = id };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<Category>(sql: sql, param: parameter,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<Category> List()
        {
            string sql = "list_categories";

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<Category>(sql: sql,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int Update(Category category)
        {
            string sql = "update_category";
            var parameter = new { @id = category.Id, @name = category.Name };

            using (IDbConnection con = OpenConnection(ConnectionStrings.CommerceStorage))
            {
                return con.Query<int>(sql: sql, param: parameter,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
