using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Services.Category;
using Pemacy.Svada.Generator.Services.Category.Exceptions;
using Pemacy.Svada.Generator.Web.Controllers.Category.Models;

namespace Pemacy.Svada.Generator.Web.Controllers.Category
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryConverter _categoryConverter;

        public CategoryController(ICategoryService categoryService, ICategoryConverter categoryConverter)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _categoryConverter = categoryConverter ?? throw new ArgumentNullException(nameof(categoryConverter));
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> List()
        {
            var categories = await _categoryService.List();
            var response = _categoryConverter.ToResponse(categories);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CategoryCreateRequest request)
        {
            var createCategoryModel = _categoryConverter.ToModel(request);
            var category = await _categoryService.Create(createCategoryModel);
            var response = _categoryConverter.ToResponse(category);

            return CreatedAtAction(nameof(Read), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> Read(int id)
        {
            var category = await TryRead(id);
            if (category == null)
            {
                return NotFound($"A category with ID {id} was not found.");
            }

            var response = _categoryConverter.ToResponse(category);
            return Ok(response);
        }

        private async Task<CategoryModel> TryRead(int id)
        {
            try
            {
                return await _categoryService.Read(id);
            }
            catch (CategoryNotFoundException)
            {
                return null;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponse>> Update(int id, [FromBody] UpdateCategoryRequest request)
        {
            var updateCategory = _categoryConverter.ToModel(id, request);
            var category = await TryUpdate(updateCategory);
            if (category == null)
            {
                return NotFound($"A category with ID {id} was not found.");
            }

            var response = _categoryConverter.ToResponse(category);
            return Ok(response);
        }


        private async Task<CategoryModel> TryUpdate(CategoryModel category)
        {
            try
            {
                return await _categoryService.Update(category);
            }
            catch (CategoryNotFoundException)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.Delete(id);
                return NoContent();
            }
            catch (CategoryNotFoundException)
            {
                return NotFound($"A category with ID {id} was not found.");
            }
        }
    }
}
