using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pemacy.Svada.Generator.Models;

namespace Pemacy.Svada.Generator.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection _dbConnection;

        public CategoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<List<CategoryModel>> List()
        {
            const string query = "SELECT * FROM Category";
            var result = await _dbConnection.QueryAsync<CategoryModel>(query);
            return result.ToList();
        }

        public async Task<CategoryModel> Create(CategoryModel categoryModel)
        {
            const string query = @"
                INSERT INTO Category(Name)
                VALUES(@Name)
                RETURNING Id, Name";
            var result = await _dbConnection.QuerySingleAsync<CategoryModel>(query, categoryModel);
            return result;
        }

        public async Task<CategoryModel> Read(int id)
        {
            const string query = "SELECT * FROM Category WHERE Id = @Id";
            var result = await _dbConnection.QuerySingleAsync<CategoryModel>(query, new {Id = id});
            return result;
        }

        public async Task<CategoryModel> Update(CategoryModel updateCategory)
        {
            const string query = @"
                UPDATE Category SET Name = @Name WHERE Id = @Id;
                SELECT * FROM Category WHERE Id = @Id;";
            var result = await _dbConnection.QuerySingleAsync<CategoryModel>(query, updateCategory);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            const string query = "DELETE FROM Category WHERE Id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, new {Id = id});
            return rowsAffected == 1;
        }
    }
}
