using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pemacy.Svada.Generator.Models;
using Pemacy.Svada.Generator.Repositories;
using Pemacy.Svada.Generator.Services.Sentence.Exceptions;

namespace Pemacy.Svada.Generator.Services.Sentence
{
    public interface ISentenceGenerator
    {
        Task<string> Generate(int categoryId);
    }

    public class SentenceGenerator : ISentenceGenerator
    {
        private readonly IWordRepository _wordRepository;

        public SentenceGenerator(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository ?? throw new ArgumentNullException(nameof(wordRepository));
        }

        public async Task<string> Generate(int categoryId)
        {
            var words = await _wordRepository.List(categoryId);

            var result = new StringBuilder();
            for (var i = 0; i < 7; i++)
            {
                var phrase = GetRandomPhrase(words, i);
                if (string.IsNullOrEmpty(phrase))
                {
                    throw new CategoryNotCompleteException();
                }

                result.Append($"{phrase} ");
            }

            return result.ToString().TrimEnd() + ".";
        }

        private string GetRandomPhrase(List<WordModel> words, int position)
        {
            var phrases = words.Where(model => model.SentencePosition == position).ToList();
            var random = new Random();
            return phrases[random.Next(phrases.Count)].Content;
        }
    }
}
