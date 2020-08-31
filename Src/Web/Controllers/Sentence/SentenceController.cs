using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pemacy.Svada.Generator.Services.Sentence;
using Pemacy.Svada.Generator.Services.Sentence.Exceptions;

namespace Pemacy.Svada.Generator.Web.Controllers.Sentence
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SentenceController : ControllerBase
    {
        private readonly ISentenceGenerator _sentenceGenerator;

        public SentenceController(ISentenceGenerator sentenceGenerator)
        {
            _sentenceGenerator = sentenceGenerator ?? throw new ArgumentNullException(nameof(sentenceGenerator));
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<string>> GenerateSentence(int categoryId)
        {
            try
            {
                var sentence = await _sentenceGenerator.Generate(categoryId);
                return Ok(sentence);
            }
            catch (CategoryNotCompleteException)
            {
                return Problem(
                    "Words need to be added in certain positions for category in order to produce full sentences.");
            }
        }
    }
}
