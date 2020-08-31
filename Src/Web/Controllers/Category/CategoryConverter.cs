using System;
using System.Collections.Generic;
using System.Linq;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Web.Controllers.Category.Models;

namespace Pemacy.Svada.Generator.Web.Controllers.Category
{
    public class CategoryConverter : ICategoryConverter
    {
        public List<CategoryResponse> ToResponse(List<CategoryModel> categoryModelList)
            => categoryModelList.Select(ToResponse).ToList();

        public CategoryModel ToModel(CategoryCreateRequest request)
            => new CategoryModel
            {
                Name = request.Name,
            };

        public CategoryModel ToModel(int id, UpdateCategoryRequest categoryRequest)
            => new CategoryModel
            {
                Id = id,
                Name = categoryRequest.Name,
            };

        public CategoryResponse ToResponse(CategoryModel categoryModel)
        {
            _ = categoryModel ?? throw new ArgumentNullException(nameof(categoryModel));

            return new CategoryResponse
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name
            };
        }
    }
}
