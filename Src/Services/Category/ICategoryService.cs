using System.Collections.Generic;
using System.Threading.Tasks;
using Pemacy.Svada.Generator.Models;

namespace Pemacy.Svada.Generator.Services.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> List();

        Task<CategoryModel> Create(CategoryModel categoryModel);
        Task<CategoryModel> Read(int id);
        Task<CategoryModel> Update(CategoryModel updateCategory);
        Task Delete(int id);
    }
}
