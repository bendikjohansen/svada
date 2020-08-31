using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Repositories;
using Pemacy.Svada.Generator.Services.Category;
using Xunit;

namespace Pemacy.Svada.Generator.Services.UnitTests
{
    public class CategoryServiceTest
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryServiceTest()
        {
            _categoryRepository = A.Fake<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepository);
        }

        [Fact]
        public async Task Test_category_service_returns_list_from_repository()
        {
            var expectedResult = new List<CategoryModel>();
            A.CallTo(() => _categoryRepository.List()).Returns(expectedResult);
            var result = await _categoryService.List();
            result.Should().BeSameAs(expectedResult);
        }
    }
}
