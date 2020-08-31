using System.Collections.Generic;
using System.Threading.Tasks;
using Pemacy.Svada.Generator.Models;

namespace Pemacy.Svada.Generator.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> List();
        Task<CategoryModel> Create(CategoryModel categoryModel);
        Task<CategoryModel> Read(int id);
        Task<CategoryModel> Update(CategoryModel updateCategory);
        Task<bool> Delete(int id);
    }
}
