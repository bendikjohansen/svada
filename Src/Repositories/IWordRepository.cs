using System.Collections.Generic;
using System.Threading.Tasks;
using Pemacy.Svada.Generator.Models;

namespace Pemacy.Svada.Generator.Repositories
{
    public interface IWordRepository
    {
        Task<List<WordModel>> List(int categoryId);
        Task<WordModel> Create(WordModel model);
        Task<WordModel> Read(int categoryId, int id);
        Task<WordModel> Update(int categoryId, int id, WordModel model);
        Task<bool> Delete(int categoryId, int id);
    }
}
