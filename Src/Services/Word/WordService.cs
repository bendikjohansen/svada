using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Repositories;
using Pemacy.Svada.Generator.Services.Word.Exceptions;

namespace Pemacy.Svada.Generator.Services.Word
{
    public class WordService : IWordService
    {
        private IWordRepository _repository;

        public WordService(IWordRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<WordModel>> List(int categoryId)
            => _repository.List(categoryId);

        public Task<WordModel> Create(WordModel model)
            => _repository.Create(model);

        public async Task<WordModel> Read(int categoryId, int id)
        {
            try
            {
                return await _repository.Read(categoryId, id);
            }
            catch (InvalidOperationException)
            {
                throw new WordNotFoundException();
            }
        }

        public async Task<WordModel> Update(int categoryId, int id, WordModel model)
        {
            try
            {
                return await _repository.Update(categoryId, id, model);
            }
            catch (InvalidOperationException)
            {
                throw new WordNotFoundException();
            }
        }

        public Task Delete(int categoryId, int id)
            => _repository.Delete(categoryId, id);
    }
}
