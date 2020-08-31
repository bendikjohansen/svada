using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Web.Controllers.Category;
using Pemacy.Svada.Generator.Web.Controllers.Category.Models;
using Xunit;

namespace Pemacy.Svada.Generator.Web.UnitTests.Controllers.Category
{
    public class CategoryConverterTest
    {
        private readonly ICategoryConverter _categoryConverter = new CategoryConverter();

        [Fact]
        public void Test_ToListResponse_id_is_converted()
        {
            var category = new CategoryModel {Id = 5};
            var categoryList = new List<CategoryModel> {category};
            var responseList = _categoryConverter.ToResponse(categoryList);
            responseList.First().Id.Should().Be(category.Id);
        }

        [Fact]
        public void Test_ToListResponse_list_cannot_be_null()
        {
            List<CategoryResponse> Convert() => _categoryConverter.ToResponse(null as List<CategoryModel>);
            Assert.Throws<ArgumentNullException>(Convert);
        }

        [Fact]
        public void Test_ToListResponse_list_cannot_contain_null()
        {
            var categoryList = new List<CategoryModel> {null};
            List<CategoryResponse> Convert() => _categoryConverter.ToResponse(categoryList);
            Assert.Throws<ArgumentNullException>(Convert);
        }
    }
}
