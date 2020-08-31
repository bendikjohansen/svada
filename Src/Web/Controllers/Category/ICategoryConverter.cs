using System.Collections.Generic;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Web.Controllers.Category.Models;

namespace Pemacy.Svada.Generator.Web.Controllers.Category
{
    public interface ICategoryConverter
    {
        List<CategoryResponse> ToResponse(List<CategoryModel> categoryModelList);

        CategoryResponse ToResponse(CategoryModel categoryModel);

        CategoryModel ToModel(CategoryCreateRequest request);

        CategoryModel ToModel(int id, UpdateCategoryRequest categoryRequest);
    }
}
