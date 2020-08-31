using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pemacy.Svada.Generator.Models;

namespace Pemacy.Svada.Generator.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly IDbConnection _dbConnection;

        public WordRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<List<WordModel>> List(int categoryId)
        {
            const string query = "SELECT * FROM Word";
            var result = await _dbConnection.QueryAsync<WordModel>(query);
            return result.ToList();
        }

        public async Task<WordModel> Create(WordModel model)
        {
            const string query = @"
                INSERT INTO Word(Content, SentencePosition, CategoryId)
                VALUES(@Content, @SentencePosition, @CategoryId)
                RETURNING Id, Content, SentencePosition, CategoryId;";
            var result = await _dbConnection.QuerySingleAsync<WordModel>(query, model);
            return result;
        }

        public async Task<WordModel> Read(int categoryId, int id)
        {
            const string query = "SELECT * FROM Word WHERE Id = @Id AND CategoryId = @categoryId";
            var result = await _dbConnection.QuerySingleAsync<WordModel>(query, new {Id = id, CategoryId = categoryId});
            return result;
        }

        public async Task<WordModel> Update(int categoryId, int id, WordModel model)
        {
            const string query = @"
                UPDATE Word SET Content = @Content, SentencePosition = @SentencePosition WHERE Id = @Id;
                SELECT * FROM Word WHERE Id = @Id AND CategoryId = @categoryId;";
            var result = await _dbConnection.QuerySingleAsync<WordModel>(query, model);
            return result;
        }

        public async Task<bool> Delete(int categoryId, int id)
        {
            const string query = "DELETE FROM Word WHERE Id = @Id AND CategoryId = @categoryId;";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, new {Id = id});
            return rowsAffected == 1;
        }
    }
}
