using System;

namespace Pemacy.Svada.Generator.Services.Word.Exceptions
{
    public class WordNotFoundException : Exception
    {
        private new const string Message = "No such word could be found.";
        public WordNotFoundException() : base(Message)
        {
        }
    }
}
