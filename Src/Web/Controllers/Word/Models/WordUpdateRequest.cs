using System.ComponentModel.DataAnnotations;

namespace Pemacy.Svada.Generator.Web.Controllers.Word.Models
{
    public class WordUpdateRequest
    {
        [MaxLength(64)]
        public string Content { get; set; }
        [Range(0, 6)]
        public int SentencePosition { get; set; }
    }
}
