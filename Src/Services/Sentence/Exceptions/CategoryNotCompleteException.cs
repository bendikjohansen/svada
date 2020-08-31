using System;

namespace Pemacy.Svada.Generator.Services.Sentence.Exceptions
{
    public class CategoryNotCompleteException : Exception
    {
        public CategoryNotCompleteException() : base("Category is missing words")
        {
        }
    }
}
