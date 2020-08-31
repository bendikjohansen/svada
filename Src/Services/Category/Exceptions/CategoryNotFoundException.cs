using System;

namespace Pemacy.Svada.Generator.Services.Category.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int id) : base($"Category with ID {id} was not found.")
        {
        }
    }
}
