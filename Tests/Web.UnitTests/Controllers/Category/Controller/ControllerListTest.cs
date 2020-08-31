using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Services.Category;
using Pemacy.Svada.Generator.Web.Controllers.Category;
using Pemacy.Svada.Generator.Web.Controllers.Category.Models;
using Xunit;

namespace Pemacy.Svada.Generator.Web.UnitTests.Controllers.Category.Controller
{
    public class ControllerListTest
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryConverter _categoryConverter;
        private readonly CategoryController _categoryController;

        public ControllerListTest()
        {
            _categoryService = A.Fake<ICategoryService>();
            _categoryConverter = A.Fake<ICategoryConverter>();
            _categoryController = new CategoryController(_categoryService, _categoryConverter);
        }

        [Fact]
        public async Task Test_controller_lists_out_all_categories_from_service()
        {
            var categoryList = new List<CategoryModel>();
            var expectedResult = new List<CategoryResponse>();
            A.CallTo(() => _categoryService.List()).Returns(categoryList);
            A.CallTo(() => _categoryConverter.ToResponse(categoryList)).Returns(expectedResult);

            var response = await _categoryController.List();

            response.Result.Should().BeAssignableTo<OkObjectResult>();
            var result = response.Result.As<OkObjectResult>();
            result.Value.Should().BeAssignableTo<List<CategoryResponse>>();
            result.Value.As<List<CategoryResponse>>().Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Test_controller_creates_category()
        {
            var request = new CategoryCreateRequest();
            var model = new CategoryModel();
            var expectedResult = new CategoryModel();
            A.CallTo(() => _categoryConverter.ToModel(request)).Returns(model);
            A.CallTo(() => _categoryService.Create(model)).Returns(expectedResult);

            var result = await _categoryController.Create(request);
        }
    }
}
