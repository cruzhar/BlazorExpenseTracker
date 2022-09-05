using BlazorExpenseTracker.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data.Repositories
{
    public class CategoryRepository : IcategoryRepository
    {
        private SqlConfiguration _connectionString;

        public CategoryRepository(SqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }


        public async Task<bool> DeleteCategory(int id)
        {
            var db = dbConnection();
            var sql = @"Delete Categories where Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;

        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {


            var db = dbConnection();
            var sql = @"select Id, Name From Categories";
            return await db.QueryAsync<Category>(sql, new { }); 
        }

        public async Task<Category> GetCategoryDetails(int id)
        {

            var db = dbConnection();
            var sql = @"select Id, Name From Categories where id=@Id";
            return await db.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });

        }

        public async Task<bool> InsertCategory(Category category)
        {
            var db = dbConnection();
            var sql = @"insert into Categories (Name) values (@Name)";
            var result = await db.ExecuteAsync(sql, new { category.Name });
            return result > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {


            var db = dbConnection();
            var sql = @"updateCategories set Name = @Name where Id = @Id";
            var result = await db.ExecuteAsync(sql, new { category.Name, category.Id });
            return result > 0;


        }
    }
}
