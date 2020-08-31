using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pemacy.Svada.Generator.Services.Word;
using Pemacy.Svada.Generator.Services.Word.Exceptions;
using Pemacy.Svada.Generator.Web.Controllers.Word.Models;

namespace Pemacy.Svada.Generator.Web.Controllers.Word
{
    [ApiController]
    [Route("api/category/{categoryId}/[controller]")]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly IWordConverter _wordConverter;

        public WordController(IWordService wordService, IWordConverter wordConverter)
        {
            _wordService = wordService ?? throw new ArgumentNullException(nameof(wordService));
            _wordConverter = wordConverter ?? throw new ArgumentNullException(nameof(wordConverter));
        }

        [HttpGet]
        public async Task<ActionResult<List<WordResponse>>> List(int categoryId)
        {
            var words = await _wordService.List(categoryId);
            var response = words.Select(_wordConverter.Convert).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<WordResponse>> Create(int categoryId, [FromBody] WordCreateRequest request)
        {
            var model = _wordConverter.Convert(request, categoryId);
            var word = await _wordService.Create(model);
            var response = _wordConverter.Convert(word);

            return CreatedAtAction(nameof(Read), new {categoryId, response.Id}, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordResponse>> Read(int categoryId, int id)
        {
            try
            {
                var word = await _wordService.Read(categoryId, id);
                var response = _wordConverter.Convert(word);

                return Ok(response);
            }
            catch (WordNotFoundException)
            {
                return NotFound($"No word with id {id} could be found.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WordResponse>> Update(int categoryId, int id,
            [FromBody] WordUpdateRequest request)
        {
            try
            {
                var model = _wordConverter.Convert(request, categoryId);
                var word = await _wordService.Update(categoryId, id, model);
                var response = _wordConverter.Convert(word);

                return CreatedAtAction(nameof(Read), response);
            }
            catch (WordNotFoundException)
            {
                return NotFound($"No word with id {id} could be found.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int categoryId, int id)
        {
            await _wordService.Delete(categoryId, id);

            return NoContent();
        }
    }
}
