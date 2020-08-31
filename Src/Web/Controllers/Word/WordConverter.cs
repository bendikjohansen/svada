using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Web.Controllers.Word.Models;

namespace Pemacy.Svada.Generator.Web.Controllers.Word
{
    public class WordConverter : IWordConverter
    {
        public WordResponse Convert(WordModel model)
            => new WordResponse
            {
                Id = model.Id,
                Content = model.Content,
                CategoryId = model.CategoryId,
                SentencePosition = model.SentencePosition
            };

        public WordModel Convert(WordCreateRequest request, int categoryId)
            => new WordModel
            {
                Content = request.Content,
                CategoryId = categoryId,
                SentencePosition = request.SentencePosition
            };

        public WordModel Convert(WordUpdateRequest request, int categoryId)
            => new WordModel
            {
                Content = request.Content,
                CategoryId = categoryId,
                SentencePosition = request.SentencePosition
            };
    }
}
