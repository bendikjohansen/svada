using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Repositories;
using Pemacy.Svada.Generator.Services.Category.Exceptions;

namespace Pemacy.Svada.Generator.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public Task<List<CategoryModel>> List()
            => _categoryRepository.List();

        public Task<CategoryModel> Create(CategoryModel categoryModel)
            => _categoryRepository.Create(categoryModel);

        public async Task<CategoryModel> Read(int id)
        {
            try
            {
                return await _categoryRepository.Read(id);
            }
            catch (InvalidOperationException)
            {
                throw new CategoryNotFoundException(id);
            }
        }

        public async Task<CategoryModel> Update(CategoryModel updateCategory)
        {
            try
            {
                return await _categoryRepository.Update(updateCategory);
            }
            catch (InvalidOperationException)
            {
                throw new CategoryNotFoundException(updateCategory.Id);
            }
        }

        public async Task Delete(int id)
        {
            var deleted = await _categoryRepository.Delete(id);
            if (!deleted) {
                throw new CategoryNotFoundException(id);
            }
        }
    }
}
